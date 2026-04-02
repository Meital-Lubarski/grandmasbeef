using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Type 'Player1' or 'Player2' here")]
    [SerializeField] private string controlScheme = "Player1";

    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float minLaunchForce = 4f;
    [SerializeField] private float maxLaunchForce = 14f;
    [SerializeField] private float maxChargeTime = 1.5f;

    private InputSystem_Actions _inputActions;
    private float _chargeStartTime;
    private bool _isCharging;
    private GameObject _currentProjectile;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.bindingMask = InputBinding.MaskByGroup(controlScheme);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.PlayerMovement.Shoot.started += OnShootStarted;
        _inputActions.PlayerMovement.Shoot.canceled += OnShootCanceled;
    }

    private void OnDisable()
    {
        _inputActions.PlayerMovement.Shoot.started -= OnShootStarted;
        _inputActions.PlayerMovement.Shoot.canceled -= OnShootCanceled;
        _inputActions.Disable();
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (_currentProjectile != null)
        {
            return;
        }
        _isCharging = true;
        _chargeStartTime = Time.time;
        EventManagement.OnSlingshotAimStarted?.Invoke();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        if (!_isCharging)
        {
            return;
        }

        _isCharging = false;

        if (_currentProjectile != null)
        {
            return;
        }
        float heldTime = Time.time - _chargeStartTime;
        float chargePercent = Mathf.Clamp01(heldTime / maxChargeTime);
        float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, chargePercent);
        
        Vector2 shootDirection = transform.up;

        EventManagement.OnSlingshotAiming?.Invoke(shootDirection, chargePercent);

        GameObject projectileObject =
            Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        _currentProjectile = projectileObject;

        SlingshotProjectile projectile =
            projectileObject.GetComponent<SlingshotProjectile>();

        if (projectile != null)
        {
            projectile.Initialize(shootDirection, launchForce, this);
        }
    }

    public void NotifyProjectileDestroyed()
    {
        _currentProjectile = null;
    }
}
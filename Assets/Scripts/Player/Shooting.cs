using System.Collections.Generic;
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
    [Tooltip("Min should be not less than 1.6 - the player will hit himself")]
    [SerializeField] private float minLaunchForce = 4f;
    [SerializeField] private float maxLaunchForce = 14f;
    [SerializeField] private float maxChargeTime = 1.5f;

    private InputSystem_Actions _inputActions;
    private float _chargeStartTime;
    private bool _isCharging;
    private GameObject _currentProjectile;
    
    private bool _canShoot = false;
    
    public bool IsCharging => _isCharging;
    public Transform ShootPoint => shootPoint;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.bindingMask = InputBinding.MaskByGroup(controlScheme);
        
        AssignPlayerDevices();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.PlayerMovement.Shoot.started += OnShootStarted;
        _inputActions.PlayerMovement.Shoot.canceled += OnShootCanceled;
        
        EventManagement.SetShootingEnabled += ToggleShooting;
    }

    private void OnDisable()
    {
        _inputActions.PlayerMovement.Shoot.started -= OnShootStarted;
        _inputActions.PlayerMovement.Shoot.canceled -= OnShootCanceled;
        _inputActions.Disable();
        
        EventManagement.SetShootingEnabled -= ToggleShooting;
    }

    private void ToggleShooting(bool isEnabled)
    {
        _canShoot = isEnabled;
    }
    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (!_canShoot) return;
        if (_currentProjectile != null) return;
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
        Debug.Log("shooting in force " + launchForce);
        Vector2 shootDirection = transform.up;
        EventManagement.OnSlingshotAiming?.Invoke(shootDirection, chargePercent);
        GameObject projectileObject =
            Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        EventManagement.OnProjectileSpawned?.Invoke();
        EventManagement.OnPlayerShot?.Invoke(controlScheme);
        _currentProjectile = projectileObject;
        SlingshotProjectile projectile =
            projectileObject.GetComponent<SlingshotProjectile>();
        if (projectile != null)
        {
            Collider2D playerCollider = GetComponent<Collider2D>(); 
            projectile.Initialize(shootDirection, launchForce, this, playerCollider);
        }
    }

    public void NotifyProjectileDestroyed()
    {
        _currentProjectile = null;
    }
    
    //Assign ps4 controllers for each player based on their control scheme
    private void AssignPlayerDevices()
    {
        List<InputDevice> myDevices = new List<InputDevice>();

        if (Keyboard.current != null)
        {
            myDevices.Add(Keyboard.current);
        }

        if (Gamepad.all.Count > 0)
        {
            if (controlScheme == "Player1")
            {
                myDevices.Add(Gamepad.all[0]);
            }
            else if (controlScheme == "Player2" && Gamepad.all.Count > 1)
            {
                myDevices.Add(Gamepad.all[1]);
            }
        }
        
        if (myDevices.Count > 0)
        {
            _inputActions.devices = myDevices.ToArray();
        }
    }
}
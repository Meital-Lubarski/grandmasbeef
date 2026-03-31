using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Type 'Player1' or 'Player2' here")]
    [SerializeField] private string controlScheme = "Player1"; 

    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.bindingMask = InputBinding.MaskByGroup(controlScheme);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        
        _inputActions.PlayerMovement.Shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        _inputActions.PlayerMovement.Shoot.performed -= OnShoot;
        _inputActions.Disable();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        //TODO---
    }
}

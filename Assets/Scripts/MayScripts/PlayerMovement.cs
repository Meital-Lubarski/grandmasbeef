using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Type 'Player1' or 'Player2' here")]
    [SerializeField] private string controlScheme = "Player1"; 
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;

    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;
    
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.bindingMask = InputBinding.MaskByGroup(controlScheme);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        
        _inputActions.PlayerMovement.Move.performed += OnMove;
        _inputActions.PlayerMovement.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        _inputActions.PlayerMovement.Move.performed -= OnMove;
        _inputActions.PlayerMovement.Move.canceled -= OnMove;        
        _inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (CanMove) 
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (_moveInput.y != 0)
        {
            transform.Translate(Vector3.up * (_moveInput.y * moveSpeed * Time.deltaTime));
        }
        if (_moveInput.x != 0)
        {
            transform.Rotate(Vector3.forward * (-_moveInput.x * rotationSpeed * Time.deltaTime));
        }
    }
}
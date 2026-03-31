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
    
    private Rigidbody2D _rb;

    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.bindingMask = InputBinding.MaskByGroup(controlScheme);
        
        _rb = GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        if (CanMove) 
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (_moveInput.x != 0)
        {
            float rotation = -_moveInput.x * rotationSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation + rotation);
        }
        if (_moveInput.y != 0)
        {
            Vector2 moveDirection = transform.up * (_moveInput.y * moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + moveDirection);
        }
    }
}
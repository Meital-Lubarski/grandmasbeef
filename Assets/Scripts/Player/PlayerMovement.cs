using System.Collections.Generic;
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
    
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    
    private Rigidbody2D _rb;
    
    public string PlayerId => controlScheme;

    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.bindingMask = InputBinding.MaskByGroup(controlScheme);
        
        AssignPlayerDevices();        
        
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    { 
        //saving the initial location of the players
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        
        _inputActions.PlayerMovement.Move.performed += OnMove;
        _inputActions.PlayerMovement.Move.canceled += OnMove;
        
        EventManagement.OnResetPositions += ResetPlayer;
    }

    private void OnDisable()
    {
        _inputActions.PlayerMovement.Move.performed -= OnMove;
        _inputActions.PlayerMovement.Move.canceled -= OnMove;        
        _inputActions.Disable();
        
        EventManagement.OnResetPositions -= ResetPlayer;
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
    
    private void ResetPlayer()
    { 
        //Moving the player back into initial position
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
    private void AssignPlayerDevices()
    {
        List<InputDevice> myDevices = new List<InputDevice>();

        // הוספת מקלדת כברירת מחדל
        if (Keyboard.current != null)
        {
            myDevices.Add(Keyboard.current);
        }

        // הוספת שלטים לפי ה-Control Scheme
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

        // עדכון המכשירים ב-Input Actions
        if (myDevices.Count > 0)
        {
            _inputActions.devices = myDevices.ToArray();
        }
    }
}
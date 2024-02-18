using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // declare reference variables
    private CharacterController _characterController;
    public CharacterController CharacterController { get { return _characterController; } }
    private Camera _cam;
    Animator _animator;
    PlayerActions _playerActions;

    // variables to store optimized setter/getter parameter IDs
    int _isWalkingHash;
    int _isRunningHash;

    // variables to store player input values
    Vector3 _currentMovementInput;
    float _currentMovementY;
    Vector3 _appliedMovement;
    bool _isMovementPressed;
    bool _isRunPressed;
    Vector3 _moveDirection;
    public Vector3 MoveDirection { get { return _moveDirection; } }

    // constants
    float _walkSpeed = 10.0f;
    float _runSpeed = 15.0f;
    private float _turnSmooth = 10.0f;
    int _zero = 0;

    // Jumping Variables
    float _maxJumpHeight = 4.0f;
    float _maxJumpTime = .75f;
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    bool _isJumping = false;
    int _isJumpingHash;
    int _jumpCountHash;
    bool _requireNewJumpPress = false;
    int _jumpCount = 0;
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine _currentJumpResetRoutine = null;

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public Animator Animator { get { return _animator; } }
    public Coroutine CurrentJumpResetRoutine { get { return _currentJumpResetRoutine; } set { _currentJumpResetRoutine = value; } }
    public Dictionary<int, float> InitialJumpVelocities { get { return _initialJumpVelocities; } }
    public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public float RunSpeed { get { return _runSpeed; } }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public int JumpCountHash { get { return _jumpCountHash; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public float CurrentMovementY { get { return _currentMovementY; } set { _currentMovementY = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }

    void Awake()
    {
        _cam = FindObjectOfType<Camera>();
        _playerActions = new PlayerActions();
        _characterController = GetComponent<CharacterController>();
        //_animator = GetComponent<Animator>();

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        /*_isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");*/

        _playerActions.Player_Map.Movement.started += OnMovementInput;
        _playerActions.Player_Map.Movement.canceled += OnMovementInput;
        _playerActions.Player_Map.Movement.performed += OnMovementInput;
        _playerActions.Player_Map.Run.started += OnRun;
        _playerActions.Player_Map.Run.canceled += OnRun;
        _playerActions.Player_Map.Jump.started += OnJump;
        _playerActions.Player_Map.Jump.canceled += OnJump;

        SetJumpVariables();
    }
    private void Start()
    {
        _characterController.Move(_appliedMovement * Time.deltaTime);
    }
    private void Update()
    {
        HandleRotation();
        _currentState.UpdateStates();
        _characterController.Move(_appliedMovement * Time.deltaTime);
    }
    void HandleRotation()
    {
        _moveDirection = new Vector3(_currentMovementInput.x, 0, _currentMovementInput.z).normalized;
        _moveDirection = Quaternion.AngleAxis(_cam.transform.rotation.eulerAngles.y, Vector3.up) * _moveDirection;
        //_moveDirection = Vector3.ClampMagnitude(_moveDirection, 1f);
        Quaternion currentRotation = transform.rotation;
        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _turnSmooth * Time.deltaTime);
        }
    }
    void SetJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        float initialGravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, initialGravity);
        _jumpGravities.Add(1, initialGravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector3>().normalized;
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.z != _zero;
    }
    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }
    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }
    void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }
}
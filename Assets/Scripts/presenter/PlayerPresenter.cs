using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    public Transform cam;
    private CharacterController _characterController;
    private float _speed = 6.0f;
    private float _run = 3.0f;
    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    
    private float _vSpeed = 0f; // Current vertical speed after jumping
    private float _jumpSpeed = 11.0f;
    private float _jumpModifier = 1.3f;
    private float _gravity = 9.8f;
    private float _gravityAmplifier = 2.1f;


    private PlayerActions _playerActions;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    void Awake()
    {
        _playerActions = new PlayerActions();
    }
    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }
    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    void Update()
    {
        Vector3 direction = _playerActions.Player_Map.Movement.ReadValue<Vector3>().normalized;
        bool running = _playerActions.Player_Map.Run.IsPressed();
        CalculateVSpeed(running);
        if (direction.magnitude >= 0.1f)
        {
            // Found online black magic for finding x, y velocity and have camera follow
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            var newSpeed = running && _characterController.isGrounded ? _run * _speed : _speed;
            moveDir.x = moveDir.x * newSpeed;
            moveDir.z = moveDir.z * newSpeed;
            moveDir.y = _vSpeed;
            _characterController.Move(moveDir * Time.deltaTime);
        }
        else
        {
            direction.y = _vSpeed;
            _characterController.Move(direction * Time.deltaTime);
        }
    }
    private void CalculateVSpeed(bool running)
    {
        if (_characterController.isGrounded)
        {
            _vSpeed = 0;
            if (_playerActions.Player_Map.Jump.IsPressed())
            {
                _vSpeed = running ? _jumpModifier * _jumpSpeed : _jumpSpeed;
            }
        }
        _vSpeed -= _gravity * _gravityAmplifier * Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Wall":
                EventManager.RaiseOnDamageTaken(.001f);
                break;
            default:
                break;
        }
    }
}

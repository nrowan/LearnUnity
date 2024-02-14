using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    GameObject _myGameObject;
    Transform _myTransform;
    Transform _playerTransform;
    NavMeshAgent _agent;
    float _rotationSpeed;
    float _walkSpeed;
    float _chaseSpeed;
    float _patrolingSpeed;
    float _patrolRadius;
    float _attackDistance;
    float _visionDistance;
    float _awareDistance;

    TextMeshPro _text;

    public EnemyStateMachine(GameObject gameObject, Transform myTransform, Transform player, NavMeshAgent agent, float walkSpeed, float chaseSpeed, float patrolingSpeed,
        float patrolingRadius, float rotationSpeed, float attackDistance, float visionDistance, float awareDistance)
    {
        _myGameObject = gameObject;
        _myTransform = myTransform;
        _playerTransform = player;
        _agent = agent;
        _walkSpeed = walkSpeed;
        _chaseSpeed = chaseSpeed;
        _patrolingSpeed = patrolingSpeed;
        _patrolRadius = patrolingRadius;
        _rotationSpeed = rotationSpeed;
        _attackDistance = attackDistance;
        _visionDistance = visionDistance;
        _awareDistance = awareDistance;

        // setup state
        _states = new EnemyStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _myEyeTransform = _myGameObject.GetComponentsInChildren<Transform>().FirstOrDefault((tran) => tran.name.ToLower().Contains("eye"));
        _text = _myGameObject.GetComponentsInChildren<TextMeshPro>().FirstOrDefault((tran) => tran.name.ToLower().Contains("what"));
        Debug.Log(_text);
        
        _patrolCenter = myTransform.position;
    }
    Transform _myEyeTransform;
    public Transform MyTransform { get { return _myTransform; } set { _myTransform = value; } }
    public NavMeshAgent Agent { get { return _agent; } }
    public Transform Player { get { return _playerTransform; } }
    public float RotationSpeed { get { return _rotationSpeed; } }

    public float PatrolingSpeed { get { return _patrolingSpeed; } }
    public float PatrolRadius { get { return _patrolRadius; } }
    public float WalkSpeed { get { return _walkSpeed; } }

    bool _isPlayerInAwareRange = false;
    bool _isPlayerInFront = false;
    bool _isPlayerInVision = false;
    public bool IsPlayerInVision { get { return _isPlayerInVision; } }
    bool _isChasing = false;
    public bool IsChasing { get { return _isChasing; } set { _isChasing = value; } }
    public float ChaseSpeed { get { return _chaseSpeed; } }
    bool _isAttacking = false;
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public float AttackDistance { get { return _attackDistance; } }

    Vector3 _patrolCenter; // Original location for patrol, so they can return
    public Vector3 PatrolCenter { get { return _patrolCenter; } }
    bool _destinationSet = false;
    public bool DestinationSet { get { return _destinationSet; } set { _destinationSet = value; } }
    Vector3 _destination; // Randomized patrol destination
    public Vector3 Destination { get { return _destination; } set { _destination = value; } }
    public float DestinationY { set { _destination.y = value; } }
    EnemyBaseState _currentState;
    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    EnemyStateFactory _states;

    public void Update()
    {
        if(_text != null && _currentState._currentSubState != null)
        {
            _text.text = _currentState._currentSubState.ToString() + "\nis aware: " + 
                _isPlayerInAwareRange + "\nis in front: " + _isPlayerInFront + "\nis vision: " + _isPlayerInVision;
        }
        CheckPlayerLocation();
        _currentState.UpdateStates();
    }

    void CheckPlayerLocation()
    {
        int layerId = 3;
        int layerMask = 1 << layerId;
        // Find if player collider is near enemy by overlapsphere
        Collider[] hitColliders = Physics.OverlapSphere(_myTransform.position, _awareDistance, layerMask);
        if (hitColliders.Length != 0)
        {
            _isPlayerInAwareRange = true;
            // Find out if player is in front of enemy by using dot angles
            Vector3 toTarget = (_playerTransform.position - _myTransform.position).normalized;
            if (Vector3.Dot(toTarget, _myTransform.transform.forward) > .7)
            {
                _isPlayerInFront = true;
                // Can enemy see player using a ray. This will account for objects in the way and distance of vision
                Ray ray = new Ray(_myEyeTransform.position, toTarget);
                RaycastHit hitData;
                Physics.Raycast(ray, out hitData, _visionDistance);
                Debug.DrawRay(ray.origin, ray.direction * _visionDistance);
                if (hitData.collider != null && hitData.collider.tag == "Player")
                {
                    _isPlayerInVision = true;
                }
                else
                {
                    _isPlayerInVision = false;
                }
            }
            else
            {
                _isPlayerInVision = false;
                _isPlayerInFront = false;
            }
        }
        else
        {
            _isPlayerInVision = false;
            _isPlayerInFront = false;
            _isPlayerInAwareRange = false;
        }
    }
}
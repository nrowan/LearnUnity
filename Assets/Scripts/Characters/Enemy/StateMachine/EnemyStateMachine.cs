using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    EnemyMovementSO _enemyMovement;
    GameObject _myGameObject;
    Transform _myTransform;
    Transform _playerTransform;
    NavMeshAgent _agent;

    TextMeshPro _text;

    public EnemyStateMachine(GameObject gameObject, Transform myTransform, Transform player, NavMeshAgent agent, EnemyMovementSO enemyMovementSO)
    {
        _myGameObject = gameObject;
        _myTransform = myTransform;
        _playerTransform = player;
        _agent = agent;
        _enemyMovement = enemyMovementSO;

        // setup state
        _states = new EnemyStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _myEyeTransform = _myGameObject.GetComponentsInChildren<Transform>().FirstOrDefault((tran) => tran.name.ToLower().Contains("eye"));
        _text = _myGameObject.GetComponentsInChildren<TextMeshPro>().FirstOrDefault((tran) => tran.name.ToLower().Contains("what"));
        _patrolCenter = myTransform.position;
    }
    Transform _myEyeTransform;
    public Transform MyTransform { get { return _myTransform; } set { _myTransform = value; } }
    public NavMeshAgent Agent { get { return _agent; } }
    public Transform Player { get { return _playerTransform; } }
    public float RotationSpeed { get { return _enemyMovement.RotationSpeed; } }

    public float PatrolingSpeed { get { return _enemyMovement.PatrolingSpeed; } }
    public float PatrolRadius { get { return _enemyMovement.PatrolRadius; } }
    public float WalkSpeed { get { return _enemyMovement.WalkSpeed; } }

    bool _isPlayerInAwareRange = false;
    bool _isPlayerInFront = false;
    bool _isPlayerInVision = false;
    public bool IsPlayerInVision { get { return _isPlayerInVision; } }
    float? _playerLastSeenTime = null;
    public float? PlayerLastSeenTime { get { return _playerLastSeenTime; } set { _playerLastSeenTime = value; } }
    Vector3 _playerLastLocation;
    public Vector3 PlayerLastLocation { get { return _playerLastLocation; } }
    bool _isChasing = false;
    public bool IsChasing { get { return _isChasing; } set { _isChasing = value; } }
    public float ChaseSpeed { get { return _enemyMovement.ChaseSpeed; } }
    bool _isAttacking = false;
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public float AttackDistance { get { return _enemyMovement.AttackDistance; } }
    bool _isHunting = false;
    public bool IsHunting { get { return _isHunting; } set { _isHunting = value; } }

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
        if (_text != null && _currentState._currentSubState != null)
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
        Collider[] hitColliders = Physics.OverlapSphere(_myTransform.position, _enemyMovement.AwareDistance, layerMask);
        if (hitColliders.Length != 0)
        {
            _isPlayerInAwareRange = true;
            // Find out if player is in front of enemy by using dot angles
            Vector3 toTarget = _playerTransform.position - _myTransform.position;
            if (Vector3.Dot(toTarget, _myTransform.transform.forward) > .7)
            {
                _isPlayerInFront = true;
                // Can enemy see player using a ray. This will account for objects in the way and distance of vision
                Ray ray = new Ray(_myEyeTransform.position, toTarget);
                RaycastHit hitData;
                Physics.Raycast(ray, out hitData, _enemyMovement.VisionDistance);
                Debug.DrawRay(ray.origin, ray.direction * _enemyMovement.VisionDistance);
                if (hitData.collider != null && hitData.collider.tag == "Player")
                {
                    _isPlayerInVision = true;
                    _playerLastSeenTime = Time.time;
                    _playerLastLocation = _playerTransform.position;
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
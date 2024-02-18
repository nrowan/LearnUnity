using UnityEngine;
using UnityEngine.AI;

public class EnemyMartian : MonoBehaviour
{
    EnemyStateMachine _stateMachine;
    Transform _player;
    NavMeshAgent _agent;
    public float _rotationSpeed;
    public float _walkSpeed;
    public float _chaseSpeed;
    public float _patrolingSpeed;
    public float _patrolRadius;
    public float _awareDistance;
    public float _visionDistance;
    public float _attackDistance;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;
        _agent.angularSpeed = 1000.0f;
        _stateMachine = new EnemyStateMachine(gameObject, transform, _player, _agent, _walkSpeed,
            _chaseSpeed, _patrolingSpeed, _patrolRadius, _rotationSpeed, _attackDistance, _visionDistance, _awareDistance);
    }
    private void Update()
    {
        _stateMachine.Update();
    }
}
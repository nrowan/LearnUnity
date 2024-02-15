using UnityEngine;
using UnityEngine.AI;

public class EnemyMartian : MonoBehaviour
{
    EnemyStateMachine _stateMachine;
    Transform _player;
    NavMeshAgent _agent;
    public float _rotationSpeed = 200.0f;
    public float _walkSpeed = 5.0f;
    public float _chaseSpeed = 8.0f;
    public float _patrolingSpeed = 10.0f;
    public float _patrolRadius = 10.0f;
    public float _awareDistance = 50.0f;
    public float _visionDistance = 5.0f;
    public float _attackDistance = 6.0f;

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
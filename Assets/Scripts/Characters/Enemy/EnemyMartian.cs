using UnityEngine;
using UnityEngine.AI;

public class EnemyMartian : MonoBehaviour
{
    [SerializeField]
    EnemyMovementSO _enemyMovement;
    EnemyStateMachine _stateMachine;
    Transform _player;
    NavMeshAgent _agent;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;
        _agent.angularSpeed = 1000.0f;
        _stateMachine = new EnemyStateMachine(gameObject, transform, _player, _agent, _enemyMovement);
    }
    private void Update()
    {
        _stateMachine.Update();
    }
}
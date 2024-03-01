using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMovement", menuName = "Enemy/EnemyMovement", order = 2)]
public class EnemyMovementSO : ScriptableObject {
    [field: SerializeField]
    public float WalkSpeed { get; set; }
    [field: SerializeField]
    public float ChaseSpeed { get; set; }
    [field: SerializeField]
    public float PatrolingSpeed { get; set; }
    [field: SerializeField]
    public float PatrolRadius { get; set; }
    [field: SerializeField]
    public float AwareDistance { get; set; }
    [field: SerializeField]
    public float VisionDistance { get; set; }
    [field: SerializeField]
    public float AttackDistance { get; set; }
    [field: SerializeField]
    public float RotationSpeed { get; set; }
}
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovement", menuName = "Player/Player Movement", order = 0)]
public class PlayerMovementSO : ScriptableObject
{
    [field: SerializeField]
    public float WalkSpeed { get; set; }
    [field: SerializeField]
    public float RunSpeed { get; set; }
    [field: SerializeField]
    public float TurnSmooth { get; set; }
    [field: SerializeField]
    public float MaxJumpHeight { get; set; }
    [field: SerializeField]
    public int MaxJumpCount { get; set; }
}
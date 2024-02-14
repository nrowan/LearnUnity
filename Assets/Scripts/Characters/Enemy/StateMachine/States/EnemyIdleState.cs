using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    float _delay = 5.0f;
    float _delayDeadline = 0f;
    public EnemyIdleState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState()
    {
        // Pick new destination after small delay
        if (Ctx.DestinationSet == false && Time.time >= _delayDeadline)
        {
            _delayDeadline = Time.time + _delay;
            Ctx.Destination = GetRandomPositionInRadius(Ctx.PatrolRadius);
            Ctx.DestinationY = Ctx.MyTransform.position.y;
            Ctx.DestinationSet = true;
        }
        CheckSwitchStates();
    }
    private Vector3 GetRandomPositionInRadius(float radius)
    {
        float randomZ = Random.Range(-radius, Ctx.PatrolRadius);
        float randomX = Random.Range(-radius, radius);
        Vector3 position = new Vector3(Ctx.PatrolCenter.x + randomX, Ctx.PatrolCenter.y, Ctx.MyTransform.position.z + randomZ);
        return position;
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsPlayerInVision && Ctx.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsPlayerInVision || Ctx.IsChasing)
        {
            Ctx.IsChasing = true;
            Ctx.DestinationSet = false;
            SwitchState(Factory.Chase());
        }
        else if (Ctx.DestinationSet)
        {
            SwitchState(Factory.Patroling());
        }
    }
    public override void InitializeSubState() { }
}
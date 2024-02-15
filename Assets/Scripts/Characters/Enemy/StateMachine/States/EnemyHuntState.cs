using UnityEngine;

public class EnemyHuntState : EnemyBaseState
{
    float _timeToHunt = 10.0f;
    public EnemyHuntState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
        Ctx.Agent.isStopped = true;
    }
    public override void UpdateState()
    {
        if (Ctx.IsPlayerInVision)
        {
            Ctx.IsHunting = false;
            Ctx.IsChasing = true;
        }
        else
        {
            if (Ctx.PlayerLastSeenTime == null || Time.time - Ctx.PlayerLastSeenTime > _timeToHunt)
            {
                Ctx.IsHunting = false;
            }
        }
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsChasing)
        {
            SwitchState(Factory.Chase());
        }
        else if (!Ctx.IsHunting && !Ctx.IsChasing && !Ctx.IsAttacking && !Ctx.DestinationSet)
        {
            SwitchState(Factory.Idle());
        }
        else if (!Ctx.IsHunting && !Ctx.IsChasing && !Ctx.IsAttacking && Ctx.DestinationSet)
        {
            SwitchState(Factory.Patroling());
        }
    }
    public override void InitializeSubState() { }
}
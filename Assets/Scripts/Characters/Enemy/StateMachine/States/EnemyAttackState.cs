using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory) { }
    public override void EnterState()
    {
        Ctx.Agent.isStopped = true;
    }
    public override void UpdateState()
    {
        if (Ctx.IsPlayerInVision)
        {
            // If greater than attack range stop attacking
            if (Vector3.Distance(Ctx.Player.position, Ctx.MyTransform.position) > Ctx.AttackDistance)
            {
                Ctx.IsAttacking = false;
                Ctx.IsChasing = true;
            }
            else
            {
                LookAtTarget(Ctx.Player.position);
            }
        }
        else
        {
            Ctx.IsAttacking = false;
            Ctx.IsChasing = true;
        }

        CheckSwitchStates();
    }
    private void LookAtTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - Ctx.MyTransform.position;
        lookPos.y = Ctx.MyTransform.position.y;
        Quaternion rotation = Quaternion.LookRotation(lookPos.normalized);
        Ctx.MyTransform.rotation = Quaternion.Slerp(Ctx.MyTransform.rotation, rotation, 0.2f);
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsChasing)
        {
            SwitchState(Factory.Chase());
        }
        if (!Ctx.IsAttacking && !Ctx.DestinationSet)
        {
            SwitchState(Factory.Idle());
        }
        else if (!Ctx.IsAttacking && Ctx.DestinationSet)
        {
            SwitchState(Factory.Patroling());
        }
    }
    public override void InitializeSubState() { }
}
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    float _pathUpdateDelay = 0.2f;
    float _pathUpdateDeadline = 0f;
    public EnemyChaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.ChaseSpeed;
    }
    public override void UpdateState()
    {
        if (Ctx.IsPlayerInVision)
        {
            // If outside the attack range, pursue player
            if (Vector3.Distance(Ctx.Player.position, Ctx.MyTransform.position) >= Ctx.AttackDistance)
            {
                if (Time.time >= _pathUpdateDeadline)
                {
                    Vector3 destination = Ctx.Player.position;
                    destination.y = Ctx.MyTransform.position.y;
                    _pathUpdateDeadline = Time.time + _pathUpdateDelay;
                    Ctx.Agent.SetDestination(destination);
                }
            }
            else
            {
                Ctx.IsChasing = false;
                Ctx.IsAttacking = true;
            }
        }
        else
        {
            Ctx.IsChasing = false;
            Ctx.Agent.isStopped = true;
            Ctx.IsHunting = true;
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
        else if (Ctx.IsHunting)
        {
            SwitchState(Factory.Hunt());
        }
        else if (!Ctx.IsChasing && !Ctx.IsAttacking && !Ctx.DestinationSet)
        {
            SwitchState(Factory.Idle());
        }
        else if (!Ctx.IsChasing && !Ctx.IsAttacking && Ctx.DestinationSet)
        {
            SwitchState(Factory.Patroling());
        }
    }
    public override void InitializeSubState() { }
}
using UnityEngine;

public class EnemyPatrolingState : EnemyBaseState
{
    float _pathUpdateDelay = 0.8f;
    float _pathUpdateDeadline = 0f;
    public EnemyPatrolingState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory) { }
    public override void EnterState()
    {
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.PatrolingSpeed;
    }

    public override void UpdateState()
    {
        // If at destination find new destination
        if (Ctx.DestinationSet && Vector3.Distance(Ctx.Destination, Ctx.MyTransform.position) < 1.5f)
        {
            Ctx.DestinationSet = false;
        }
        else if(Ctx.DestinationSet)
        {
            // Go back to patrol spot but watch for better paths pretty infrequently
            if (Time.time >= _pathUpdateDeadline)
            {
                _pathUpdateDeadline = Time.time + _pathUpdateDelay;
                Ctx.Agent.SetDestination(Ctx.Destination);
            }
        }
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsPlayerInVision)
        {
            Ctx.IsChasing = true;
            Ctx.DestinationSet = false;
            SwitchState(Factory.Chase());
        }
        else if (!Ctx.DestinationSet)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState() { }
}
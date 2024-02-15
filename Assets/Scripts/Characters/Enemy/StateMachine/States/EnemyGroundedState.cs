

public class EnemyGroundedState : EnemyBaseState, IRootState
{
    public EnemyGroundedState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
        IsRootState = true;
    }
    public void HandleGravity() { }
    public override void EnterState()
    {
        InitializeSubState();
        HandleGravity();
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void InitializeSubState()
    {
        if(Ctx.IsAttacking)
        {
            SetSubState(Factory.Attack());
        }
        else if (Ctx.IsChasing)
        {
            SetSubState(Factory.Chase());
        }
        else if(Ctx.DestinationSet)
        {
            SetSubState(Factory.Patroling());
        }
        else if (!Ctx.DestinationSet)
        {
            SetSubState(Factory.Idle());
        }
    }
    public override void CheckSwitchStates()
    {

    }
}
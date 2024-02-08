public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        //Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        //Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
    }
    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CamMoveDirection.x * Ctx.RunMultiplier;
        Ctx.AppliedMovementZ = Ctx.CamMoveDirection.z * Ctx.RunMultiplier;
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState() { }
}
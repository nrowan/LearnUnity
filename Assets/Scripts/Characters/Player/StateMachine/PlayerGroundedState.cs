
public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    public void HandleGravity()
    {
        Ctx.CurrentMovementY = GravityHelper.Gravity;
        Ctx.AppliedMovementY = GravityHelper.Gravity;
    }
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
        if(!Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Fall());
        }
        else if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
        else if(!Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Fall());
        }
    }
}
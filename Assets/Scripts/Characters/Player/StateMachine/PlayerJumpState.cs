using System.Collections;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    IEnumerator IJumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        Ctx.JumpCount = 0;
    }

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        HandleJump();
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        HandleGravity();
    }
    public override void ExitState()
    {
        //Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        if(Ctx.IsJumpPressed)
        {
            Ctx.RequireNewJumpPress = true;
        }
        Ctx.CurrentJumpResetRoutine = Ctx.StartCoroutine(IJumpResetRoutine());
        if (Ctx.JumpCount == 3)
        {
            Ctx.JumpCount = 0;
            //Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
        }
    }
    public override void CheckSwitchStates() 
    {
        if(Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }
    public override void InitializeSubState() { }

    void HandleJump()
    {
        if (Ctx.JumpCount < 3 && Ctx.CurrentJumpResetRoutine != null)
        {
            Ctx.StopCoroutine(Ctx.CurrentJumpResetRoutine);
        }
        //Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        Ctx.RequireNewJumpPress = true;
        Ctx.IsJumping = true;
        Ctx.JumpCount += 1;
        //Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
        Ctx.CurrentMovemntY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
        Ctx.AppliedMovementY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
    }

    void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovemntY <= 0.0f || !Ctx.IsJumpPressed;
        float fallMultiplier = 2.0f;

        if(isFalling)
        {
            float previousYVelocity = Ctx.CurrentMovemntY;
            Ctx.CurrentMovemntY = Ctx.CurrentMovemntY + (Ctx.JumpGravities[Ctx.JumpCount] * fallMultiplier * Time.deltaTime);
            Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovemntY) * .5f, -20.0f);
        }
        else{
            float previousYVelocity = Ctx.CurrentMovemntY;
            Ctx.CurrentMovemntY = Ctx.CurrentMovemntY + (Ctx.JumpGravities[Ctx.JumpCount] * Time.deltaTime);
            Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovemntY) * .5f;
        }
    }
}
using System.Collections;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState, IRootState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        InitializeSubState();
        HandleJump();
    }
    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        //Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        if (Ctx.IsJumpPressed)
        {
            Ctx.RequireNewJumpPress = true;
        }
        Ctx.CurrentJumpResetRoutine = Ctx.StartCoroutine(IJumpResetRoutine());
        if (Ctx.JumpCount == Ctx.MaxJumpCount)
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
        if (Ctx.JumpCount < Ctx.MaxJumpCount && Ctx.CurrentJumpResetRoutine != null)
        {
            Ctx.StopCoroutine(Ctx.CurrentJumpResetRoutine);
        }
        //Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        Ctx.RequireNewJumpPress = true;
        Ctx.IsJumping = true;
        Ctx.JumpCount += 1;
        //Ctx.Animator.SetInteger(Ctx.JumpCountHash, Ctx.JumpCount);
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
        Ctx.AppliedMovementY = Ctx.InitialJumpVelocities[Ctx.JumpCount];
    }

    public void HandleGravity()
    {
        bool isFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;
        float fallMultiplier = 2.0f;

        if(isFalling)
        {
            float previousYVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.JumpGravities[Ctx.JumpCount] * fallMultiplier * Time.deltaTime);
            Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * .5f, -20.0f);
        }
        else{
            float previousYVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.JumpGravities[Ctx.JumpCount] * Time.deltaTime);
            Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * .5f;
        }
    }
    IEnumerator IJumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        Ctx.JumpCount = 0;
    }
}
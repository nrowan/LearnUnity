using UnityEngine;

public class EnemyHuntState : EnemyBaseState
{
    float _timeToHunt = 20.0f;
    float _pathUpdateDelay = 0.5f;
    float _pathUpdateDeadline = 0f;
    Vector3 _from = new Vector3(0, 0 , 0);
    Vector3 _to = new Vector3(0.0F, 0.0F, 0.0F);
    float _frequency = .2F;

    public EnemyHuntState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory)
    {
    }
    public override void EnterState()
    {
        Ctx.Agent.isStopped = false;
    }
    public override void UpdateState()
    {
        if (Ctx.IsPlayerInVision)
        {
            // Player is still in vision go back to chasing
            Ctx.IsHunting = false;
            Ctx.IsChasing = true;
        }
        else
        {
            if (Ctx.PlayerLastSeenTime == null || Time.time - Ctx.PlayerLastSeenTime > _timeToHunt)
            {
                // Player hasn't been found in hunting time, stop hunting
                Ctx.IsHunting = false;
                Ctx.Agent.isStopped = true;
            }
            else
            {
                // Still in hunting time, check distance to last known player location
                if (Vector3.Distance(Ctx.PlayerLastLocation, Ctx.MyTransform.position) >= 1.5f)
                {
                    // Only update path semi frequently to destination to prevent too much cycles
                    // Set destination with one call likely would be fine, but account for things getting in the way
                    if (Time.time >= _pathUpdateDeadline)
                    {
                        // Move enemy to last seen position
                        Vector3 destination = Ctx.PlayerLastLocation;
                        destination.y = Ctx.MyTransform.position.y;
                        _pathUpdateDeadline = Time.time + _pathUpdateDelay;
                        Ctx.Agent.SetDestination(destination);
                    }
                    //_from = Ctx.MyTransform.eulerAngles;
                }
                else
                {
                    // At last known location, look around
                    //Quaternion targetRotation = Quaternion.LookRotation(Ctx.Player.position);
                    //Ctx.MyTransform.rotation = Quaternion.Slerp(Ctx.MyTransform.rotation, targetRotation, 2 * Time.deltaTime);
                    /*Quaternion from = Quaternion.Euler(_from);
                    Quaternion to = Quaternion.Euler(_to);

                    float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * _frequency));
                    Ctx.MyTransform.localRotation = Quaternion.Lerp(from, to, lerp);*/
                }
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
        else if (!Ctx.IsHunting)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState() { }
}
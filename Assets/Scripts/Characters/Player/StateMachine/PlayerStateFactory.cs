using System.Collections.Generic;

enum PlayerStates
{
    idle,
    walk,
    run,
    grounded,
    jump,
    fall
}
public class PlayerStateFactory
{
    PlayerStateMachine _context;
    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        _states.Add(PlayerStates.idle, new PlayerIdleState(_context, this));
        _states.Add(PlayerStates.walk, new PlayerWalkState(_context, this));
        _states.Add(PlayerStates.run, new PlayerRunState(_context, this));
        _states.Add(PlayerStates.jump, new PlayerJumpState(_context, this));
        _states.Add(PlayerStates.fall, new PlayerFallState(_context, this));
        _states.Add(PlayerStates.grounded, new PlayerGroundedState(_context, this));
    }
    public PlayerBaseState Idle()
    {
        return _states[PlayerStates.idle];
    }
    public PlayerBaseState Walk()
    {
        return _states[PlayerStates.walk];
    }
    public PlayerBaseState Run()
    {
        return _states[PlayerStates.run];
    }
    public PlayerBaseState Jump()
    {
        return _states[PlayerStates.jump];
    }
    public PlayerBaseState Fall()
    {
        return _states[PlayerStates.fall];
    }
    public PlayerBaseState Grounded()
    {
        return _states[PlayerStates.grounded];
    }
}
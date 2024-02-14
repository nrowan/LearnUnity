using System.Collections.Generic;

enum EnemyStates
{
    patroling,
    idle,
    attack,
    chase,
    grounded,
}
public class EnemyStateFactory
{
    EnemyStateMachine _context;
    Dictionary<EnemyStates, EnemyBaseState> _states = new Dictionary<EnemyStates, EnemyBaseState>();
    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
        _states.Add(EnemyStates.patroling, new EnemyPatrolingState(_context, this));
        _states.Add(EnemyStates.idle, new EnemyIdleState(_context, this));
        _states.Add(EnemyStates.chase, new EnemyChaseState(_context, this));
        _states.Add(EnemyStates.attack, new EnemyAttackState(_context, this));
        _states.Add(EnemyStates.grounded, new EnemyGroundedState(_context, this));
    }
    public EnemyBaseState Patroling()
    {
        return _states[EnemyStates.patroling];
    }
    public EnemyBaseState Idle()
    {
        return _states[EnemyStates.idle];
    }
    public EnemyBaseState Chase()
    {
        return _states[EnemyStates.chase];
    }
    public EnemyBaseState Grounded()
    {
        return _states[EnemyStates.grounded];
    }
    public EnemyBaseState Attack()
    {
        return _states[EnemyStates.attack];
    }
}
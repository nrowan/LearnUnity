using UnityEngine;

public abstract class EnemyBaseState
{
    private bool _isRootState = false;
    protected bool IsRootState { get { return _isRootState; } set { _isRootState = value; } }
    protected EnemyBaseState CurrentSubState { get { return _currentSubState; } }
    private EnemyStateMachine _ctx;
    protected EnemyStateMachine Ctx { get { return _ctx; } }
    private EnemyStateFactory _factory;
    protected EnemyStateFactory Factory { get { return _factory; } }
    public EnemyBaseState _currentSubState;
    private EnemyBaseState _currentSuperState;

    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    {
        _ctx = currentContext;
        _factory = enemyStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }
    public void ExitStates()
    {
        ExitState();
        if (_currentSubState != null)
        {
            _currentSubState.ExitStates();
        }
    }
    protected void SwitchState(EnemyBaseState newState)
    {
        // current state exists state
        ExitState();
        newState.EnterState();
        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(EnemyBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(EnemyBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
        _currentSubState.EnterState();
    }
}
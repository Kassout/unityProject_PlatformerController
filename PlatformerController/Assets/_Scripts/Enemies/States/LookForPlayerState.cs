using UnityEngine;

public class LookForPlayerState : State
{
    protected bool _turnImmediately;
    protected bool _isPlayerInMinAggroRange;
    protected bool _isAllTurnsDone;
    protected bool _isAllTurnsTimeDone;

    protected int _amountOfTurnsDone;

    protected float _lastTurnTime;
    
    protected LookForPlayerStateData _stateData;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName,
        LookForPlayerStateData stateData) 
        : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isAllTurnsDone = false;
        _isAllTurnsTimeDone = false;

        _lastTurnTime = StartTime;
        _amountOfTurnsDone = 0;

        Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(0f);
        
        if (_turnImmediately)
        {
            Movement.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
            _turnImmediately = false;
        }
        else if (Time.time >= _lastTurnTime + _stateData.timeBetweenTurns && !_isAllTurnsDone)
        {
            Movement.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
        }

        if (_amountOfTurnsDone >= _stateData.amountOfTurns)
        {
            _isAllTurnsDone = true;
        }

        if (Time.time >= _lastTurnTime + _stateData.timeBetweenTurns && _isAllTurnsDone)
        {
            _isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isPlayerInMinAggroRange = _core.GetCoreComponent<EnemySenses>().PlayerInMinAggroRange;
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}

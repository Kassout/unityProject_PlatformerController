using UnityEngine;

public class IdleState : State
{
    protected bool _flipAfterIdle;
    protected bool _isIdleTimeOver;
    protected bool _isPlayerInMinAggroRange;

    protected float _idleTime;
    
    protected IdleStateData _stateData;
    
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, IdleStateData stateData) 
        : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _entity.SetVelocity(0f);
        _isIdleTimeOver = false;
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
        {
            _entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _startTime + _idleTime)
        {
            _isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        _flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        _idleTime = Random.Range(_stateData.minIdleTime, _stateData.maxIdleTime);
    }
}

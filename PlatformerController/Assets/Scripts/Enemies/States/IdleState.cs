using UnityEngine;

public class IdleState : State
{
    protected bool _flipAfterIdle;
    protected bool _isIdleTimeOver;
    protected bool _isPlayerInMinAggroRange;

    protected float _idleTime;
    
    protected IdleStateData _stateData;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, IdleStateData stateData) 
        : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Movement.SetVelocityX(0f);
        _isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
        {
            Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement.SetVelocityX(0f);

        if (Time.time >= StartTime + _idleTime)
        {
            _isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
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

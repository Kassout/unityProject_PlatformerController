using UnityEngine;

public class StunState : State
{
    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _performCloseRangeAction;
    protected bool _isPlayerInMinAggroRange;
    
    protected StunStateData _stateData;
    
    public StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, StunStateData stateData) 
        : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isStunTimeOver = false;
        _isMovementStopped = false;
        _core.Movement.SetVelocity(_stateData.stunKnockBackSpeed, _stateData.stunKnockBackAngle, _entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        _entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > StartTime + _stateData.stunTime)
        {
            _isStunTimeOver = true;
        }

        if (_isGrounded && Time.time >= StartTime + _stateData.stunKnockBackTime && !_isMovementStopped)
        {
            _isMovementStopped = true;
            _core.Movement.SetVelocityX(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _core.CollisionSenses.Ground;
        _performCloseRangeAction = _entity.CheckPlayerInCloseRangeAction();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
    }
}

using UnityEngine;

public class StunState : State
{
    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _performCloseRangeAction;
    protected bool _isPlayerInMinAggroRange;
    
    protected StunStateData _stateData;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;
    
    public StunState(Entity entity, string animationBoolName, StunStateData stateData) 
        : base(entity, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isStunTimeOver = false;
        _isMovementStopped = false;
        Movement.SetVelocity(_stateData.stunKnockBackSpeed, _stateData.stunKnockBackAngle, _entity.LastDamageDirection);
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
            Movement.SetVelocityX(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = CollisionSenses.Ground;
        _performCloseRangeAction = _core.GetCoreComponent<EnemySenses>().PlayerInCloseRangeAction;
        _isPlayerInMinAggroRange = _core.GetCoreComponent<EnemySenses>().PlayerInMinAggroRange;
    }
}

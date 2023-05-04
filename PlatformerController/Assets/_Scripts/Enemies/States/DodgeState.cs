using UnityEngine;

public class DodgeState : State
{
    protected bool _performCloseRangeAction;
    protected bool _isPlayerInMaxAggroRange;
    protected bool _isGrounded;
    protected bool _isDodgeOver;
    
    protected DodgeStateData _stateData;

    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;
    
    public DodgeState(Entity entity, string animationBoolName, DodgeStateData stateData) 
        : base(entity, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isDodgeOver = false;
        
        Movement.SetVelocity(_stateData.dodgeSpeed, _stateData.dodgeAngle, -Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + _stateData.dodgeTime && _isGrounded)
        {
            _isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _performCloseRangeAction = _core.GetCoreComponent<EnemySenses>().PlayerInCloseRangeAction;
        _isPlayerInMaxAggroRange = _core.GetCoreComponent<EnemySenses>().PlayerInMaxAggroRange;
        _isGrounded = CollisionSenses.Ground;
    }
}

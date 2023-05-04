using UnityEngine;

public class ChargeState : State
{
    protected bool _isPlayerInMinAggroRange;
    protected bool _isDetectingLedge;
    protected bool _isDetectingWall;
    protected bool _isChargeTimeOver;
    protected bool _performCloseRangeAction;
    
    protected ChargeStateData _stateData;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;
    
    public ChargeState(Entity entity, string animationBoolName, ChargeStateData stateData) : base(entity, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isChargeTimeOver = false;
        Movement.SetVelocityX(_stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(_stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= StartTime + _stateData.chargeTime)
        {
            _isChargeTimeOver = true;
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
        _isDetectingLedge = CollisionSenses.LedgeVertical;
        _isDetectingWall = CollisionSenses.WallFront;
        _performCloseRangeAction = _core.GetCoreComponent<EnemySenses>().PlayerInCloseRangeAction;
    }
}

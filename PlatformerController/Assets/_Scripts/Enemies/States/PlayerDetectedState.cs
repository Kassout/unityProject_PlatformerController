using UnityEngine;

public class PlayerDetectedState : State
{
    protected PlayerDetectedStateData _stateData;

    protected bool _isPlayerInMinAggroRange;
    protected bool _isPlayerInMaxAggroRange;
    protected bool _performLongRangeAction;
    protected bool _performCloseRangeAction;
    protected bool _isDetectingLedge;
    
    protected Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    protected Movement _movement;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;
    
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, PlayerDetectedStateData stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _performLongRangeAction = false;
        Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + _stateData.longRangeActionTime)
        {
            _performLongRangeAction = true;
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
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
        _performCloseRangeAction = _entity.CheckPlayerInCloseRangeAction();
        _isDetectingLedge = CollisionSenses.LedgeVertical;
    }
}
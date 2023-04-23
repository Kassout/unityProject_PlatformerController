using UnityEngine;

public class PlayerDetectedState : State
{
    protected PlayerDetectedStateData _stateData;

    protected bool _isPlayerInMinAggroRange;
    protected bool _isPlayerInMaxAggroRange;
    protected bool _performLongRangeAction;
    protected bool _performCloseRangeAction;
    protected bool _isDetectingLedge;
    
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, PlayerDetectedStateData stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _performLongRangeAction = false;
        _entity.SetVelocity(0f);
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
        _isDetectingLedge = _entity.CheckLedge();
    }
}

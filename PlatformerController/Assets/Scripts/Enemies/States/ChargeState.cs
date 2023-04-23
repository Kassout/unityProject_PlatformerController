using UnityEngine;

public class ChargeState : State
{
    protected bool _isPlayerInMinAggroRange;
    protected bool _isDetectingLedge;
    protected bool _isDetectingWall;
    protected bool _isChargeTimeOver;
    protected bool _performCloseRangeAction;
    
    protected ChargeStateData _stateData;
    
    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, ChargeStateData stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isChargeTimeOver = false;
        _entity.SetVelocity(_stateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


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
        
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _performCloseRangeAction = _entity.CheckPlayerInCloseRangeAction();
    }
}

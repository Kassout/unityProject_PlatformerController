using UnityEngine;

public class DodgeState : State
{
    protected bool _performCloseRangeAction;
    protected bool _isPlayerInMaxAggroRange;
    protected bool _isGrounded;
    protected bool _isDodgeOver;
    
    protected DodgeStateData _stateData;

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, DodgeStateData stateData) 
        : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isDodgeOver = false;
        
        _entity.SetVelocity(_stateData.dodgeSpeed, _stateData.dodgeAngle, -_entity.FacingDirection);
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

        _performCloseRangeAction = _entity.CheckPlayerInCloseRangeAction();
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
        _isGrounded = _entity.CheckGround();
    }
}

public class MoveState : State
{
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAggroRange;
    
    protected MoveStateData _stateData;
    
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, MoveStateData stateData) 
        : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _core.Movement.SetVelocityX(_stateData.movementSpeed * _core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _core.Movement.SetVelocityX(_stateData.movementSpeed * _core.Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isDetectingLedge = _core.CollisionSenses.LedgeVertical;
        _isDetectingWall = _core.CollisionSenses.WallFront;
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
    }
}

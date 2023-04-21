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
        _entity.SetVelocity(_stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
    }
}

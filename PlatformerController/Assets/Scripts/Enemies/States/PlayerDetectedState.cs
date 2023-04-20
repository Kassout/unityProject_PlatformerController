public class PlayerDetectedState : State
{
    protected PlayerDetectedStateData _stateData;

    protected bool _isPlayerInMinAggroRange;
    protected bool _isPlayerInMaxAggroRange;
    
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, PlayerDetectedStateData stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _entity.SetVelocity(0f);
        
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
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
        
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
    }
}

public class HellDogLookForPlayerState : LookForPlayerState
{
    private HellDog _hellDog;
    
    public HellDogLookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        LookForPlayerStateData stateData, HellDog hellDog) 
        : base(entity, stateMachine, animationBoolName, stateData)
    {
        _hellDog = hellDog;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAggroRange)
        {
            _stateMachine.ChangeState(_hellDog.PlayerDetectedState);
        }
        else if (_isAllTurnsTimeDone)
        {
            _stateMachine.ChangeState(_hellDog.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}

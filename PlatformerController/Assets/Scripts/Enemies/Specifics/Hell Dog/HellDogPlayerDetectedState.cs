public class HellDogPlayerDetectedState : PlayerDetectedState
{
    private HellDog _hellDog;
    
    public HellDogPlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        PlayerDetectedStateData stateData, HellDog hellDog) 
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

        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_hellDog.MeleeAttackState);
        }
        else if (_performLongRangeAction)
        {
            _stateMachine.ChangeState(_hellDog.ChargeState);
        }
        else if (!_isPlayerInMaxAggroRange)
        {
            _stateMachine.ChangeState(_hellDog.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

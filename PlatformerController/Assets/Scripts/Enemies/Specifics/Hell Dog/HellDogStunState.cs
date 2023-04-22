public class HellDogStunState : StunState
{
    private HellDog _hellDog;
    
    public HellDogStunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        StunStateData stateData, HellDog hellDog) 
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

        if (_isStunTimeOver)
        {
            if (_performCloseRangeAction)
            {
                _stateMachine.ChangeState(_hellDog.MeleeAttackState);
            }
            else if (_isPlayerInMinAggroRange)
            {
                _stateMachine.ChangeState(_hellDog.ChargeState);
            }
            else
            {
                _hellDog.LookForPlayerState.SetTurnImmediately(true);
                _stateMachine.ChangeState(_hellDog.LookForPlayerState);
            }
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

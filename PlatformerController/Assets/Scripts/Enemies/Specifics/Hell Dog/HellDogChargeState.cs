public class HellDogChargeState : ChargeState
{
    private HellDog _hellDog;
    
    public HellDogChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        ChargeStateData stateData, HellDog hellDog) 
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

        if (!_isDetectingLedge || _isDetectingWall)
        {
            _stateMachine.ChangeState(_hellDog.LookForPlayerState);
        } 
        else if (_isChargeTimeOver)
        {
            if (_isPlayerInMinAggroRange)
            {
                _stateMachine.ChangeState(_hellDog.PlayerDetectedState);
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

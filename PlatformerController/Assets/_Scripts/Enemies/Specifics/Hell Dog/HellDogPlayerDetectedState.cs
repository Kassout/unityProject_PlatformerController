public class HellDogPlayerDetectedState : PlayerDetectedState
{
    private readonly HellDog _hellDog;
    
    public HellDogPlayerDetectedState(HellDog hellDog, string animationBoolName, PlayerDetectedStateData stateData) 
        : base(hellDog, animationBoolName, stateData)
    {
        _hellDog = hellDog;
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
        else if (!_isDetectingLedge)
        {
            Movement.Flip();
            _stateMachine.ChangeState(_hellDog.MoveState);
        }
    }
}

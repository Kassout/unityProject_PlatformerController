public class HellDogChargeState : ChargeState
{
    private readonly HellDog _hellDog;
    
    public HellDogChargeState(HellDog hellDog, string animationBoolName, ChargeStateData stateData) 
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
        else if (!_isDetectingLedge || _isDetectingWall)
        {
            _stateMachine.ChangeState(_hellDog.LookForPlayerState);
        } 
        else if (_isChargeTimeOver)
        { 
            if (_isPlayerInMinAggroRange)
            {
                _stateMachine.ChangeState(_hellDog.PlayerDetectedState);
            }
            else
            {
                _stateMachine.ChangeState(_hellDog.LookForPlayerState);
            }
        }
    }
}

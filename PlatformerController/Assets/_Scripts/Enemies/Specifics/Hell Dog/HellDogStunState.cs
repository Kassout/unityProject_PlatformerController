public class HellDogStunState : StunState
{
    private readonly HellDog _hellDog;
    
    public HellDogStunState(HellDog hellDog, string animationBoolName, StunStateData stateData) 
        : base(hellDog, animationBoolName, stateData)
    {
        _hellDog = hellDog;
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
}

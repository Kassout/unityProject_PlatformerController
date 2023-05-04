public class HellDogLookForPlayerState : LookForPlayerState
{
    private readonly HellDog _hellDog;
    
    public HellDogLookForPlayerState(HellDog hellDog, string animationBoolName, LookForPlayerStateData stateData) 
        : base(hellDog, animationBoolName, stateData)
    {
        _hellDog = hellDog;
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
}

public class HellDogMoveState : MoveState
{
    private readonly HellDog _hellDog;
    
    public HellDogMoveState(HellDog hellDog, string animationBoolName, MoveStateData stateData) 
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
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _hellDog.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_hellDog.IdleState);
        }
    }
}

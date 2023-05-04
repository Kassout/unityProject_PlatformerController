public class HellDogIdleState : IdleState
{
    private readonly HellDog _hellDog;
    
    public HellDogIdleState(HellDog hellDog, string animationBoolName, IdleStateData stateData) 
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
        else if (_isIdleTimeOver)
        {
            _stateMachine.ChangeState(_hellDog.MoveState);
        }
    }
}

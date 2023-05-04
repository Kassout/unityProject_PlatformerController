public class ArcherIdleState : IdleState
{
    private readonly Archer _archer;
    
    public ArcherIdleState(Archer archer, string animationBoolName, IdleStateData stateData) 
        : base(archer, animationBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAggroRange)
        {
            _stateMachine.ChangeState(_archer.PlayerDetectedState);
        }
        else if (_isIdleTimeOver)
        {
            _stateMachine.ChangeState(_archer.MoveState);
        }
    }
}

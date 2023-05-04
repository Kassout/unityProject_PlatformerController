public class ArcherLookForPlayerState : LookForPlayerState
{
    private readonly Archer _archer;
    
    public ArcherLookForPlayerState(Archer archer, string animationBoolName, LookForPlayerStateData stateData) 
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
        else if (_isAllTurnsTimeDone)
        {
            _stateMachine.ChangeState(_archer.MoveState);
        }
    }
}

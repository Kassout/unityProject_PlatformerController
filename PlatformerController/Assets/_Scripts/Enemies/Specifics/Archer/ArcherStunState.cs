public class ArcherStunState : StunState
{
    private readonly Archer _archer;
    
    public ArcherStunState(Archer archer, string animationBoolName, StunStateData stateData) 
        : base(archer, animationBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isStunTimeOver)
        {
            if (_isPlayerInMinAggroRange)
            {
                _stateMachine.ChangeState(_archer.PlayerDetectedState);
            }
            else
            {
                _stateMachine.ChangeState(_archer.LookForPlayerState);
            }
        }
    }
}

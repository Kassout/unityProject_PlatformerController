public class ArcherMoveState : MoveState
{
    private readonly Archer _archer;
    
    public ArcherMoveState(Archer archer, string animationBoolName, MoveStateData stateData) 
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
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _archer.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_archer.IdleState);
        }
    }
}

public class ArcherDodgeState : DodgeState
{
    private readonly Archer _archer;
    
    public ArcherDodgeState(Archer archer, string animationBoolName, DodgeStateData stateData) 
        : base(archer, animationBoolName, stateData)
    {
        _archer = archer;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isDodgeOver)
        {
            if (_isPlayerInMaxAggroRange && _performCloseRangeAction)
            {
                _stateMachine.ChangeState(_archer.MeleeAttackState);
            }
            else if (_isPlayerInMaxAggroRange && !_performCloseRangeAction)
            {
                _stateMachine.ChangeState(_archer.RangedAttackState);
            }
            else if (!_isPlayerInMaxAggroRange)
            {
                _stateMachine.ChangeState(_archer.LookForPlayerState);
            }
        }
    }
}

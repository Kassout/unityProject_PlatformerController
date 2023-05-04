using UnityEngine;

public class ArcherPlayerDetectedState : PlayerDetectedState
{
    private readonly Archer _archer;
    
    public ArcherPlayerDetectedState(Archer archer, string animationBoolName, PlayerDetectedStateData stateData) 
        : base(archer, animationBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_performCloseRangeAction)
        {
            if (Time.time >= _archer.DodgeState.StartTime + _archer.DodgeStateData.dodgeCooldown)
            {
                _stateMachine.ChangeState(_archer.DodgeState);
            }
            else
            {
                _stateMachine.ChangeState(_archer.MeleeAttackState);
            }
        }
        else if (_performLongRangeAction)
        {
            _stateMachine.ChangeState(_archer.RangedAttackState);
        }
        else if (!_isPlayerInMaxAggroRange)
        {
            _stateMachine.ChangeState(_archer.LookForPlayerState);
        }
    }
}

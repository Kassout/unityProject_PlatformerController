using UnityEngine;


public class ArcherRangedAttackState : RangedAttackState
{
    private readonly Archer _archer;
    
    public ArcherRangedAttackState(Archer archer, string animationBoolName, Transform attackPosition, RangedAttackStateData stateData) 
        : base(archer, animationBoolName, attackPosition, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
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

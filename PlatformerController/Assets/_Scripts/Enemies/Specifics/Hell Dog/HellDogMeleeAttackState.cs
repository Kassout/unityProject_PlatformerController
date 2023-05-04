using UnityEngine;

public class HellDogMeleeAttackState : MeleeAttackState
{
    private readonly HellDog _hellDog;
    
    public HellDogMeleeAttackState(HellDog hellDog, string animationBoolName, Transform attackPosition, MeleeAttackStateData stateData) 
        : base(hellDog, animationBoolName, attackPosition, stateData)
    {
        _hellDog = hellDog;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
        {
            if (_isPlayerInMinAggroRange)
            {
                _stateMachine.ChangeState(_hellDog.PlayerDetectedState);   
            }
            else
            {
                _stateMachine.ChangeState(_hellDog.LookForPlayerState);
            }
        }
    }
}

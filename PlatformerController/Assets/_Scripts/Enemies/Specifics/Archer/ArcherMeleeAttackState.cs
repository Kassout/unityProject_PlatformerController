using UnityEngine;

public class ArcherMeleeAttackState : MeleeAttackState
{
    private Archer _archer;

    public ArcherMeleeAttackState(Archer archer, string animationBoolName, Transform attackPosition, MeleeAttackStateData stateData) 
        : base(archer, animationBoolName, attackPosition, stateData)
    {
        _archer = archer;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}

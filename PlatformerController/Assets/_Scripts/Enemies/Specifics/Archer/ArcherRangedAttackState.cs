using UnityEngine;


public class ArcherRangedAttackState : RangedAttackState
{
    private Archer _archer;
    
    public ArcherRangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        Transform attackPosition, RangedAttackStateData stateData, Archer archer) : base(entity, stateMachine, animationBoolName, attackPosition, stateData)
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

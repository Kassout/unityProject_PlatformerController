using UnityEngine;

public class HellDogMeleeAttackState : MeleeAttackState
{
    private HellDog _hellDog;
    
    public HellDogMeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        Transform attackPosition, MeleeAttackStateData stateData, HellDog hellDog) 
        : base(entity, stateMachine, animationBoolName, attackPosition, stateData)
    {
        _hellDog = hellDog;
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
                _stateMachine.ChangeState(_hellDog.PlayerDetectedState);   
            }
            else
            {
                _stateMachine.ChangeState(_hellDog.LookForPlayerState);
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
}

using UnityEngine;

public abstract class AttackState : State
{
    protected bool _isAnimationFinished;
    protected bool _isPlayerInMinAggroRange;
    
    protected Transform _attackPosition;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;

    protected AttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName,
        Transform attackPosition)
        : base(entity, stateMachine, animationBoolName)
    {
        _attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        _entity.AnimationToStateMachine.attackState = this;
        _isAnimationFinished = false;

        Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement.SetVelocityX(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isPlayerInMinAggroRange = _core.GetCoreComponent<EnemySenses>().PlayerInMinAggroRange;
    }

    public virtual void TriggerAttack()
    {
        
    }

    public virtual void FinishAttack()
    {
        _isAnimationFinished = true;
    }
}

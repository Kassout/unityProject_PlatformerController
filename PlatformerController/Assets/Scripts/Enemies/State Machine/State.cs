using UnityEngine;

public abstract class State
{
    public float StartTime { get; private set; }

    private readonly string _animationBoolName;

    protected readonly Entity _entity;
    protected readonly FiniteStateMachine _stateMachine;

    protected State(Entity entity, FiniteStateMachine stateMachine, string animationBoolName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        StartTime = Time.time;
        _entity.Animator.SetBool(_animationBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        _entity.Animator.SetBool(_animationBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
    }
}

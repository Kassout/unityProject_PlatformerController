using UnityEngine;

public abstract class State
{
    protected float _startTime;

    protected string _animationBoolName;

    protected FiniteStateMachine _stateMachine;
    protected Entity _entity;
    
    protected State(Entity entity, FiniteStateMachine stateMachine, string animationBoolName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        _startTime = Time.time;
        _entity.Animator.SetBool(_animationBoolName, true);
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
        
    }
}

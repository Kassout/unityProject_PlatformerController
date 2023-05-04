using UnityEngine;

public class PlayerState
{
    protected bool _isAnimationFinished;
    protected bool _isExitingState;
    
    protected float _startTime;

    protected readonly Core _core;
    protected readonly Player _player;
    protected readonly PlayerData _playerData;
    protected readonly PlayerStateMachine _stateMachine;

    private readonly string _animationBoolName;

    protected PlayerState(Player player, string animationBoolName)
    {
        _player = player;
        _core = player.Core;
        _stateMachine = player.StateMachine;
        _playerData = player.PlayerData;
        _animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        _player.Animator.SetBool(_animationBoolName, true);
        _startTime = Time.time;
        _isAnimationFinished = false;
        _isExitingState = false;
    }

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animationBoolName, false);
        _isExitingState = true;
    }

    public virtual void LogicUpdate() {}

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() {}

    public virtual void AnimationTrigger() {}

    public virtual void AnimationFinishTrigger() => _isAnimationFinished = true;
}

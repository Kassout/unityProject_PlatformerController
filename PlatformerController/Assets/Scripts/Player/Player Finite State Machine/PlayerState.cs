using UnityEngine;

public class PlayerState
{
    protected bool _isAnimationFinished;
    protected bool _isExitingState;
    
    protected float _startTime;

    protected Core _core;
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected PlayerData _playerData;

    private string _animationBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _playerData = playerData;
        _animationBoolName = animationBoolName;
        _core = player.Core;
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

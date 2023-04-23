using UnityEngine;

public class PlayerState
{
    protected float _startTime;
    
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
    }

    public virtual void Enter()
    {
        DoChecks();
        _player.Animator.SetBool(_animationBoolName, true);
        _startTime = Time.time;
    }

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animationBoolName, false);
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
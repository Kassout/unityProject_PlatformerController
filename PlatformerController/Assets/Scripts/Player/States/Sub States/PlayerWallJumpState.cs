using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _wallJumpDirection;
    
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        
        _player.InputHandler.UseJumpInput();
        _player.JumpState.ResetAmountOfJumpsLeft();
        _core.Movement.SetVelocity(_playerData.wallJumpVelocity, _playerData.wallJumpAngle, _wallJumpDirection);
        _core.Movement.CheckIfShouldFlip(_wallJumpDirection);
        _player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _player.Animator.SetFloat("yVelocity", _core.Movement.CurrentVelocity.y);
        _player.Animator.SetFloat("xVelocity", Mathf.Abs(_core.Movement.CurrentVelocity.x));

        if (Time.time >= _startTime + _playerData.wallJumpTime)
        {
            _isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -_core.Movement.FacingDirection;
        }
        else
        {
            _wallJumpDirection = _core.Movement.FacingDirection;
        }
    }
}

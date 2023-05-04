using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _wallJumpDirection;
    
    public PlayerWallJumpState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        
        _player.InputHandler.UseJumpInput();
        _player.JumpState.ResetAmountOfJumpsLeft();
        Movement.SetVelocity(_playerData.wallJumpVelocity, _playerData.wallJumpAngle, _wallJumpDirection);
        Movement.CheckIfShouldFlip(_wallJumpDirection);
        _player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        _player.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

        if (Time.time >= _startTime + _playerData.wallJumpTime)
        {
            _isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -Movement.FacingDirection;
        }
        else
        {
            _wallJumpDirection = Movement.FacingDirection;
        }
    }
}

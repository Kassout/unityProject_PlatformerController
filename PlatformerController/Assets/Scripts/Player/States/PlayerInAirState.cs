using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded;
    private bool _jumpInput;
    private bool _coyoteTime;
    private bool _isJumping;
    private bool _jumpInputStop;
    
    private int _xInput;
    
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

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

        CheckCoyoteTime();
        
        _xInput = _player.InputHandler.NormalizedInputX;
        _jumpInput = _player.InputHandler.JumpInput;
        _jumpInputStop = _player.InputHandler.JumpInputStop;

        CheckJumpMultiplier();
        
        if (_isGrounded && _player.CurrentVelocity.y < 0.01f)
        {
            _stateMachine.ChangeState(_player.LandState);
        }
        else if (_jumpInput && _player.JumpState.CanJump())
        {
            _stateMachine.ChangeState(_player.JumpState);
        }
        else
        {
            _player.CheckIfShouldFlip(_xInput);
            _player.SetVelocityX(_playerData.movementVelocity * _xInput);

            _player.Animator.SetFloat("yVelocity", _player.CurrentVelocity.y);
            _player.Animator.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                _player.SetVelocityY(_player.CurrentVelocity.y * _playerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > _startTime + _playerData.coyoteTime)
        {
            _coyoteTime = false;
            _player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumping() => _isJumping = true;
}

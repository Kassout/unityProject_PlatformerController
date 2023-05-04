using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Input
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;
    private bool _dashInput;
    
    // Checks
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isTouchingWall;
    private bool _isTouchingWallBack;
    private bool _oldIsTouchingWall;
    private bool _oldIsTouchingWallBack;
    private bool _isTouchingLedge;
    private bool _coyoteTime;
    private bool _wallJumpCoyoteTime;
   
    private int _xInput;

    private float _startWallJumpCoyoteTime;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;
    
    public PlayerInAirState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void Exit()
    {
        base.Exit();

        _oldIsTouchingWall = false;
        _oldIsTouchingWallBack = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        
        _xInput = _player.InputHandler.NormalizedInputX;
        _jumpInput = _player.InputHandler.JumpInput;
        _jumpInputStop = _player.InputHandler.JumpInputStop;
        _grabInput = _player.InputHandler.GrabInput;
        _dashInput = _player.InputHandler.DashInput;

        CheckJumpMultiplier();
        
        if (_player.InputHandler.AttackInputs[(int)CombatInputs.Primary])
        {
            _stateMachine.ChangeState(_player.PrimaryAttackState);
        }
        else if (_player.InputHandler.AttackInputs[(int)CombatInputs.Secondary])
        {
            _stateMachine.ChangeState(_player.SecondaryAttackState);
        } 
        else if (_isGrounded && Movement.CurrentVelocity.y < 0.01f)
        {
            _stateMachine.ChangeState(_player.LandState);
        }
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
        {
            _stateMachine.ChangeState(_player.LedgeClimbState);
        }
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            _isTouchingWall = CollisionSenses.WallFront;
            _player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            _stateMachine.ChangeState(_player.WallJumpState);
        }
        else if (_jumpInput && _player.JumpState.CanJump())
        {
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
        else if (_isTouchingWall && _xInput == Movement.FacingDirection && Movement.CurrentVelocity.y <= 0f)
        {
            _stateMachine.ChangeState(_player.WallSlideState);
        }
        else if (_dashInput && _player.DashState.CheckIfCanDash())
        {
            _stateMachine.ChangeState(_player.DashState);
        }
        else
        {
            Movement.CheckIfShouldFlip(_xInput);
            Movement.SetVelocityX(_playerData.movementVelocity * _xInput);

            _player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            _player.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _oldIsTouchingWall = _isTouchingWall;
        _oldIsTouchingWallBack = _isTouchingWallBack;

        _isGrounded = CollisionSenses.Ground;
        _isTouchingWall = CollisionSenses.WallFront;
        _isTouchingWallBack = CollisionSenses.WallBack;
        _isTouchingLedge = CollisionSenses.LedgeHorizontal;

        if (_isTouchingWall && !_isTouchingLedge)
        {
            _player.LedgeClimbState.SetDetectedPosition(_player.transform.position);
        }

        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_oldIsTouchingWall || _oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Movement.SetVelocityY(Movement.CurrentVelocity.y * _playerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
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

    private void CheckWallJumpCoyoteTime()
    {
        if (_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + _playerData.coyoteTime)
        {
            _wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;

    public void SetIsJumping() => _isJumping = true;
}

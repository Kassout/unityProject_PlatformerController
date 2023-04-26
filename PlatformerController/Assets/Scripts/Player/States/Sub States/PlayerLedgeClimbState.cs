using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private bool _isHanging;
    private bool _isClimbing;
    private bool _jumpInput;
    private bool _isTouchingCeiling;

    private int _xInput;
    private int _yInput;
    
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;
    private Vector2 _workspace;
    
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        
        _core.Movement.SetVelocityZero();
        _player.transform.position = _detectedPosition;
        _cornerPosition = DetermineCornerPosition();
        
        _startPosition.Set(_cornerPosition.x - (_core.Movement.FacingDirection * _playerData.startOffset.x), _cornerPosition.y - _playerData.startOffset.y);
        _stopPosition.Set(_cornerPosition.x + (_core.Movement.FacingDirection * _playerData.stopOffset.x), _cornerPosition.y + _playerData.stopOffset.y);

        _player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if (_isClimbing)
        {
            _player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
        {
            if (_isTouchingCeiling)
            {
                _stateMachine.ChangeState(_player.CrouchIdleState);
            }
            else
            {
                _stateMachine.ChangeState(_player.IdleState);   
            }
        }
        else
        {
            _xInput = _player.InputHandler.NormalizedInputX;
            _yInput = _player.InputHandler.NormalizedInputY;
            _jumpInput = _player.InputHandler.JumpInput;
        
            _core.Movement.SetVelocityZero();
            _player.transform.position = _startPosition;

            if (_xInput == _core.Movement.FacingDirection && _isHanging && !_isClimbing)
            {
                CheckForSpace();
                _isClimbing = true;
                _player.Animator.SetBool("ledgeClimb", true);
            
            }
            else if (_yInput == -1 && _isHanging && !_isClimbing)
            {
                _stateMachine.ChangeState(_player.InAirState);
            }
            else if (_jumpInput && !_isClimbing)
            {
                _player.WallJumpState.DetermineWallJumpDirection(true);
                _stateMachine.ChangeState(_player.WallJumpState);
            }
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        _player.Animator.SetBool("ledgeClimb", false);
    }
    
    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;

    private void CheckForSpace()
    {
        _isTouchingCeiling = Physics2D.Raycast(_cornerPosition + Vector2.up * 0.015f + Vector2.right * _core.Movement.FacingDirection * 0.015f,
                Vector2.up, _playerData.standColliderHeight, _core.CollisionSenses.WhatIsGround);
        
        _player.Animator.SetBool("isTouchingCeiling", _isTouchingCeiling);
    }
    
    private Vector2 DetermineCornerPosition()
    {
        var wallCheckPosition = _core.CollisionSenses.WallCheck.position;
        var ledgeCheckPosition = _core.CollisionSenses.LedgeCheck.position;
        RaycastHit2D xHit = Physics2D.Raycast(wallCheckPosition, Vector2.right * _core.Movement.FacingDirection,
            _core.CollisionSenses.WallCheckDistance, _core.CollisionSenses.WhatIsGround);
        float xDistance = xHit.distance;
        _workspace.Set((xDistance + 0.015f) * _core.Movement.FacingDirection, 0f); // 0.015f == Tolerance
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheckPosition + (Vector3)_workspace, 
            Vector2.down, ledgeCheckPosition.y - wallCheckPosition.y + 0.015f, _core.CollisionSenses.WhatIsGround);
        float yDistance = yHit.distance;
        
        _workspace.Set(wallCheckPosition.x + (xDistance * _core.Movement.FacingDirection), ledgeCheckPosition.y - yDistance);
        return _workspace;
    }
}

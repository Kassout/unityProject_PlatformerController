using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private bool _isHanging;
    private bool _isClimbing;

    private int _xInput;
    private int _yInput;
    
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;
    
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;

    public override void Enter()
    {
        base.Enter();
        
        _player.SetVelocityZero();
        _player.transform.position = _detectedPosition;
        _cornerPosition = _player.DetermineCornerPosition();
        
        _startPosition.Set(_cornerPosition.x - (_player.FacingDirection * _playerData.startOffset.x), _cornerPosition.y - _playerData.startOffset.y);
        _stopPosition.Set(_cornerPosition.x + (_player.FacingDirection * _playerData.stopOffset.x), _cornerPosition.y + _playerData.stopOffset.y);

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
            _stateMachine.ChangeState(_player.IdleState);
        }
        else
        {
            _xInput = _player.InputHandler.NormalizedInputX;
            _yInput = _player.InputHandler.NormalizedInputY;
        
            _player.SetVelocityZero();
            _player.transform.position = _startPosition;

            if (_xInput == _player.FacingDirection && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                _player.Animator.SetBool("ledgeClimb", true);
            
            }
            else if (_yInput == -1 && _isHanging && !_isClimbing)
            {
                _stateMachine.ChangeState(_player.InAirState);
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
}

using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool _isHolding;
    private bool _dashInputStop;
    
    private float _lastDashTime;

    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector2 _lastAfterImagePosition;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        _player.InputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * Movement.FacingDirection;

        Time.timeScale = _playerData.holdTimeScale;
        _startTime = Time.unscaledTime;

        _player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (Movement.CurrentVelocity.y > 0)
        {
            Movement.SetVelocityY(Movement.CurrentVelocity.y * _playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            _player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            _player.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
            
            if (_isHolding)
            {
                _dashDirectionInput = _player.InputHandler.DashDirectionInput;
                _dashInputStop = _player.InputHandler.DashInputStop;

                if (_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                _player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (_dashInputStop || Time.unscaledTime >= _startTime + _playerData.maxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1f;
                    _startTime = Time.time;
                    Movement.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    _player.Rigidbody.drag = _playerData.drag;
                    Movement.SetVelocity(_playerData.dashVelocity, _dashDirection);
                    _player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                Movement.SetVelocity(_playerData.dashVelocity, _dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= _startTime + _playerData.dashTime)
                {
                    _player.Rigidbody.drag = 0f;
                    _isAbilityDone = true;
                    _lastDashTime = Time.time;
                }
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(_player.transform.position, _lastAfterImagePosition) >=
            _playerData.distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        _lastAfterImagePosition = _player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + _playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}

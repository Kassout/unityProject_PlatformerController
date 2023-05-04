public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, string animationBoolName) : base(player, animationBoolName)
    {
        _amountOfJumpsLeft = player.PlayerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        
        _player.InputHandler.UseJumpInput();
        Movement.SetVelocityY(_playerData.jumpVelocity);
        _isAbilityDone = true;
        _amountOfJumpsLeft--;
        _player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (_amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = _playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
}

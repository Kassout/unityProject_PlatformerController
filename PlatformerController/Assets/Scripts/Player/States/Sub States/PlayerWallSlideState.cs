public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            _player.SetVelocityY(-_playerData.wallSlideVelocity);

            if (_grabInput && _yInput == 0)
            {
                _stateMachine.ChangeState(_player.WallGrabState);
            }   
        }
    }
}

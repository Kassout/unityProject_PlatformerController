public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            Movement.SetVelocityY(-_playerData.wallSlideVelocity);

            if (_grabInput && _yInput == 0)
            {
                _stateMachine.ChangeState(_player.WallGrabState);
            }   
        }
    }
}

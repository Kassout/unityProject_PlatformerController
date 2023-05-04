public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            Movement.SetVelocityY(_playerData.wallClimbVelocity);

            if (_yInput != 1f)
            {
                _stateMachine.ChangeState(_player.WallGrabState);   
            }
        }
    }
}

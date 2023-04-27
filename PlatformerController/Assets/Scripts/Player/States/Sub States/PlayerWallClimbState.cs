public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

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

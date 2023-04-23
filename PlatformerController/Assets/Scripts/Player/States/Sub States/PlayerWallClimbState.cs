public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _player.SetVelocityY(_playerData.wallClimbVelocity);

        if (_yInput != 1)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
    }
}

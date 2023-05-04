public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            if (_xInput != 0)
            {
                _stateMachine.ChangeState(_player.MoveState);
            }
            else if (_isAnimationFinished)
            {
                _stateMachine.ChangeState(_player.IdleState);
            }   
        }
    }
}

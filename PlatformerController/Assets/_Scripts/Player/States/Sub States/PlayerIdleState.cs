public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            if (_xInput != 0)
            {
                _stateMachine.ChangeState(_player.MoveState);
            }
            else if (_yInput == -1)
            {
                _stateMachine.ChangeState(_player.CrouchIdleState);
            }   
        }
    }
}

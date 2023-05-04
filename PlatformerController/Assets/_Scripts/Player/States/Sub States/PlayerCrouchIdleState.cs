public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, string animationBoolName) : base(player, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        
        Movement.SetVelocityZero();
        _player.SetColliderHeight(_playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        
        _player.SetColliderHeight(_playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            if (_xInput != 0)
            {
                _stateMachine.ChangeState(_player.CrouchMoveState);
            }
            else if (_yInput != -1 && !_isTouchingCeiling)
            {
                _stateMachine.ChangeState(_player.IdleState);
            }
        }
    }
}

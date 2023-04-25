public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
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
            _player.SetVelocityX(_playerData.crouchMovementVelocity * _player.FacingDirection);
            _player.CheckIfShouldFlip(_xInput);
            
            if (_xInput == 0)
            {
                _stateMachine.ChangeState(_player.CrouchIdleState);
            }
            else if (_yInput != -1 && !_isTouchingCeiling)
            {
                _stateMachine.ChangeState(_player.MoveState);
            }
        }
    }
}

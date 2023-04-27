public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement.CheckIfShouldFlip(_xInput);

        Movement.SetVelocityX(_playerData.movementVelocity * _xInput);

        if (!_isExitingState)
        {
            if (_xInput == 0f)
            {
                _stateMachine.ChangeState(_player.IdleState);
            }
            else if (_yInput == -1)
            {
                _stateMachine.ChangeState(_player.CrouchMoveState);
            }   
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}

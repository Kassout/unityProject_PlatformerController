public class PlayerGroundedState : PlayerState
{
    protected bool _isTouchingCeiling;
    
    protected int _xInput;
    protected int _yInput;

    private bool _jumpInput;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _grabInput;
    private bool _isTouchingLedge;
    private bool _dashInput;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        
        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = _player.InputHandler.NormalizedInputX;
        _yInput = _player.InputHandler.NormalizedInputY;
        _jumpInput = _player.InputHandler.JumpInput;
        _grabInput = _player.InputHandler.GrabInput;
        _dashInput = _player.InputHandler.DashInput;

        if (_player.InputHandler.AttackInputs[(int)CombatInputs.Primary] && !_isTouchingCeiling)
        {
            _stateMachine.ChangeState(_player.PrimaryAttackState);
        }
        else if (_player.InputHandler.AttackInputs[(int)CombatInputs.Secondary] && !_isTouchingCeiling)
        {
            _stateMachine.ChangeState(_player.SecondaryAttackState);
        } 
        else if (_jumpInput && _player.JumpState.CanJump())
        {
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (!_isGrounded)
        {
            _player.InAirState.StartCoyoteTime();
            _stateMachine.ChangeState(_player.InAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
        else if (_dashInput && _player.DashState.CheckIfCanDash() && !_isTouchingCeiling)
        {
            _stateMachine.ChangeState(_player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _core.CollisionSenses.Ground;
        _isTouchingWall = _core.CollisionSenses.WallFront;
        _isTouchingLedge = _core.CollisionSenses.LedgeHorizontal;
        _isTouchingCeiling = _core.CollisionSenses.Ceiling;
    }
}

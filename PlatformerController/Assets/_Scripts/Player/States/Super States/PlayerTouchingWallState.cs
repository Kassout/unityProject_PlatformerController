public class PlayerTouchingWallState : PlayerState
{
    protected bool _isGrounded;
    protected bool _isTouchingWall;
    protected bool _grabInput;
    protected bool _jumpInput;
    protected bool _isTouchingLedge;
    
    protected int _xInput;
    protected int _yInput;
    
    protected Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    protected Movement _movement;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;

    public PlayerTouchingWallState(Player player, string animationBoolName) : base(player, animationBoolName) {}

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

        _xInput = _player.InputHandler.NormalizedInputX;
        _yInput = _player.InputHandler.NormalizedInputY;
        _grabInput = _player.InputHandler.GrabInput;
        _jumpInput = _player.InputHandler.JumpInput;

        if (_jumpInput)
        {
            _player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            _stateMachine.ChangeState(_player.WallJumpState);
        }
        else if (_isGrounded && !_grabInput)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
        else if (!_isTouchingWall || (_xInput != Movement.FacingDirection && !_grabInput))
        {
            _stateMachine.ChangeState(_player.InAirState);
        }
        else if (_isTouchingWall && !_isTouchingLedge)
        {
            _stateMachine.ChangeState(_player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = CollisionSenses.Ground;
        _isTouchingWall = CollisionSenses.WallFront;
        _isTouchingLedge = CollisionSenses.LedgeHorizontal;

        if (_isTouchingWall && !_isTouchingLedge)
        {
            _player.LedgeClimbState.SetDetectedPosition(_player.transform.position);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}

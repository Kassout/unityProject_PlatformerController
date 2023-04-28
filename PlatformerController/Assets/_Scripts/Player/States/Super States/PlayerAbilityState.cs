public class PlayerAbilityState : PlayerState
{
    protected bool _isAbilityDone;
    
    protected Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    protected Movement _movement;

    private bool _isGrounded;
    
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private CollisionSenses _collisionSenses;
    
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();

        _isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAbilityDone)
        {
            if (_isGrounded && Movement.CurrentVelocity.y < 0.01f)
            {
                _stateMachine.ChangeState(_player.IdleState);
            }
            else
            {
                _stateMachine.ChangeState(_player.InAirState);
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

        _isGrounded = CollisionSenses.Ground;
    }
}

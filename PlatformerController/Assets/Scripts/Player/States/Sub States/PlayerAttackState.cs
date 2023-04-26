public class PlayerAttackState : PlayerAbilityState
{
    private bool _setVelocity;
    private bool _shouldCheckFlip;

    private int _xInput;
    
    private float _velocityToSet;

    private Weapon _weapon;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();

        _setVelocity = false;
        
        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        
        _weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = _player.InputHandler.NormalizedInputX;

        if (_shouldCheckFlip)
        {
            _core.Movement.CheckIfShouldFlip(_xInput);   
        }

        if (_setVelocity)
        {
            _core.Movement.SetVelocityX(_velocityToSet * _core.Movement.FacingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.InitializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        _core.Movement.SetVelocityX(velocity * _core.Movement.FacingDirection);

        _velocityToSet = velocity;
        _setVelocity = true;
    }

    public void SetFlipCheck(bool value)
    {
        _shouldCheckFlip = value;
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }

    #endregion
}
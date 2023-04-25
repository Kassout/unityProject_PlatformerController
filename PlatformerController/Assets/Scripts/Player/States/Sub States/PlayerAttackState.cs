public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}

    public override void Enter()
    {
        base.Enter();
        
        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        
        _weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.InitializeWeapon(this);
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _isAbilityDone = true;
    }

    #endregion
}

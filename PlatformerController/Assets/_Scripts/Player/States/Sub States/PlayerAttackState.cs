public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
        string animationBoolName) 
        : base(player, stateMachine, playerData, animationBoolName) {}
}

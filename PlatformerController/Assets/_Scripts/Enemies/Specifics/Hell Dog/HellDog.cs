using UnityEngine;

public class HellDog : Entity
{
    public HellDogIdleState IdleState { get; private set; }
    public HellDogMoveState MoveState { get; private set; }
    public HellDogPlayerDetectedState PlayerDetectedState { get; private set; }
    public HellDogChargeState ChargeState { get; private set; }
    public HellDogLookForPlayerState LookForPlayerState { get; private set; }
    public HellDogMeleeAttackState MeleeAttackState { get; private set; }
    public HellDogStunState StunState { get; private set; }
    public DeadState DeadState { get; private set; }

    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    [SerializeField] private ChargeStateData chargeStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private MeleeAttackStateData meleeAttackStateData;
    [SerializeField] private StunStateData stunStateData;
    [SerializeField] private DeadStateData deadStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        MoveState = new HellDogMoveState(this, "move", moveStateData);
        IdleState = new HellDogIdleState(this, "idle", idleStateData);
        PlayerDetectedState = new HellDogPlayerDetectedState(this, "playerDetected", playerDetectedStateData);
        ChargeState = new HellDogChargeState(this, "charge", chargeStateData);
        LookForPlayerState = new HellDogLookForPlayerState(this, "lookForPlayer", lookForPlayerStateData);
        MeleeAttackState = new HellDogMeleeAttackState(this, "meleeAttack", meleeAttackPosition, meleeAttackStateData);
        StunState = new HellDogStunState(this, "stun", stunStateData);
        DeadState = new DeadState(this, "dead", deadStateData);

        Core.GetCoreComponent<Stats>().OnStunResistanceZero += Stun;
    }

    private void Start()
    {
        stateMachine.Initialize(MoveState);
    }
    
    public virtual void Stun()
    {
        _isStunned = true;
        stateMachine.ChangeState(StunState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}

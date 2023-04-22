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
    public HellDogDeadState DeadState { get; private set; }

    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    [SerializeField] private ChargeStateData chargeStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private MeleeAttackStateData meleeAttackStateData;
    [SerializeField] private StunStateData stunStateData;
    [SerializeField] private DeadStateData deadStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        MoveState = new HellDogMoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new HellDogIdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new HellDogPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        ChargeState = new HellDogChargeState(this, stateMachine, "charge", chargeStateData, this);
        LookForPlayerState = new HellDogLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        MeleeAttackState = new HellDogMeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        StunState = new HellDogStunState(this, stateMachine, "stun", stunStateData, this);
        DeadState = new HellDogDeadState(this, stateMachine, "dead", deadStateData, this);
        
        stateMachine.Initialize(MoveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (_isDead)
        {
            stateMachine.ChangeState(DeadState);
        }
        else if (_isStunned && stateMachine.CurrentState != StunState)
        {
            stateMachine.ChangeState(StunState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}

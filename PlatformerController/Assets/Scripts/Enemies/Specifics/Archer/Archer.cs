using UnityEngine;

public class Archer : Entity
{
    public ArcherIdleState IdleState { get; private set; }
    public ArcherMoveState MoveState { get; private set; }
    public ArcherPlayerDetectedState PlayerDetectedState { get; private set; }
    public ArcherMeleeAttackState MeleeAttackState { get; private set; }
    public ArcherLookForPlayerState LookForPlayerState { get; private set; }
    public ArcherStunState StunState { get; private set; }
    public ArcherDeadState DeadState { get; private set; }
    public ArcherDodgeState DodgeState { get; private set; }
    public ArcherRangedAttackState RangedAttackState { get; private set; }

    public DodgeStateData DodgeStateData => dodgeStateData;
    
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;
    [SerializeField] private MeleeAttackStateData meleeAttackStateData;
    [SerializeField] private LookForPlayerStateData lookForPlayerStateData;
    [SerializeField] private StunStateData stunStateData;
    [SerializeField] private DeadStateData deadStateData;
    [SerializeField] private DodgeStateData dodgeStateData;
    [SerializeField] private RangedAttackStateData rangedAttackStateData;

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangedAttackPosition;
    
    public override void Awake()
    {
        base.Awake();        
        
        MoveState = new ArcherMoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new ArcherIdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new ArcherPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        MeleeAttackState = new ArcherMeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        LookForPlayerState = new ArcherLookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        StunState = new ArcherStunState(this, stateMachine, "stun", stunStateData, this);
        DeadState = new ArcherDeadState(this, stateMachine, "dead", deadStateData, this);
        DodgeState = new ArcherDodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        RangedAttackState =
            new ArcherRangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

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
        else if (CheckPlayerInMinAggroRange())
        {
            stateMachine.ChangeState(RangedAttackState);
        }
        else if (!CheckPlayerInMinAggroRange())
        {
            LookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(LookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}

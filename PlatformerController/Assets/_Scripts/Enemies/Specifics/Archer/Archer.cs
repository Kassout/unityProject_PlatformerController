using UnityEngine;

public class Archer : Entity
{
    public ArcherIdleState IdleState { get; private set; }
    public ArcherMoveState MoveState { get; private set; }
    public ArcherPlayerDetectedState PlayerDetectedState { get; private set; }
    public ArcherMeleeAttackState MeleeAttackState { get; private set; }
    public ArcherLookForPlayerState LookForPlayerState { get; private set; }
    public ArcherStunState StunState { get; private set; }
    public DeadState DeadState { get; private set; }
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
        
        MoveState = new ArcherMoveState(this, "move", moveStateData);
        IdleState = new ArcherIdleState(this, "idle", idleStateData);
        PlayerDetectedState = new ArcherPlayerDetectedState(this, "playerDetected", 
            playerDetectedStateData);
        MeleeAttackState = new ArcherMeleeAttackState(this, "meleeAttack", meleeAttackPosition, 
            meleeAttackStateData);
        LookForPlayerState = new ArcherLookForPlayerState(this, "lookForPlayer", 
            lookForPlayerStateData);
        StunState = new ArcherStunState(this, "stun", stunStateData);
        DeadState = new DeadState(this, "dead", deadStateData);
        DodgeState = new ArcherDodgeState(this, "dodge", dodgeStateData);
        RangedAttackState = new ArcherRangedAttackState(this, "rangedAttack", rangedAttackPosition, 
            rangedAttackStateData);
    }

    private void Start()
    {
        stateMachine.Initialize(MoveState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}

using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public EntityData entityData;
    
    public int LastDamageDirection { get; private set; }
    
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    
    protected bool _isStunned;
    protected bool _isDead;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;

    private float _currentHealth;
    private float _currentStunResistance;
    private float _lastDamageTime;
    
    private Vector2 _velocityWorkspace;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;
        
        Animator = GetComponent<Animator>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();

        Animator.SetFloat("yVelocity", Core.Movement.Rigidbody.velocity.y);
        
        if (Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, 
            entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, 
            entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, 
            entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        _velocityWorkspace.Set(Core.Movement.Rigidbody.velocity.x, velocity);
        Core.Movement.Rigidbody.velocity = _velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        _isStunned = false;
        _currentStunResistance = entityData.stunResistance;
    }

    public virtual void OnDrawGizmos()
    {
        if (Core)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * Core.Movement.FacingDirection * entityData.wallCheckDistance);
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * entityData.ledgeCheckDistance);
        
            Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.closeRangeActionDistance, 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.minAggroDistance, 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.maxAggroDistance, 0.2f);   
        }
    }
}

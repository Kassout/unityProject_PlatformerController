using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public EntityData entityData;

    public int FacingDirection { get; private set; }
    public int LastDamageDirection { get; private set; }
    
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject AliveGameObject { get; private set; }
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

    public virtual void Start()
    {
        FacingDirection = 1;
        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;
        
        AliveGameObject = transform.Find("Alive").gameObject;
        Rigidbody = AliveGameObject.GetComponent<Rigidbody2D>();
        Animator = AliveGameObject.GetComponent<Animator>();
        AnimationToStateMachine = AliveGameObject.GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();

        if (Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkspace.Set(FacingDirection * velocity, Rigidbody.velocity.y);
        Rigidbody.velocity = _velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGameObject.transform.right, 
            entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, 
            entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGameObject.transform.right, 
            entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGameObject.transform.right, 
            entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGameObject.transform.right, 
            entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        _velocityWorkspace.Set(Rigidbody.velocity.x, velocity);
        Rigidbody.velocity = _velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        _isStunned = false;
        _currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        _lastDamageTime = Time.time;
        
        _currentHealth -= attackDetails.damageAmount;
        _currentStunResistance -= attackDetails.stunDamageAmount;
        
        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, AliveGameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > AliveGameObject.transform.position.x)
        {
            LastDamageDirection = -1;
        }
        else
        {
            LastDamageDirection = 1;
        }

        if (_currentStunResistance <= 0)
        {
            _isStunned = true;
        }

        if (_currentHealth <= 0)
        {
            _isDead = true;
        }
    }
    
    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGameObject.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * FacingDirection * entityData.wallCheckDistance);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * entityData.ledgeCheckDistance);
        
        Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.closeRangeActionDistance, 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.minAggroDistance, 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + Vector3.right * entityData.maxAggroDistance, 0.2f);
    }
}

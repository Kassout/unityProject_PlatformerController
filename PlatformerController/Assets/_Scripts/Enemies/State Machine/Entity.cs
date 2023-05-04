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

    private float _currentStunResistance;
    private float _lastDamageTime;
    
    private Vector2 _velocityWorkspace;

    private Movement Movement => _movement ??= Core.GetCoreComponent<Movement>();
    private Movement _movement;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        _currentStunResistance = entityData.stunResistance;
        
        Animator = GetComponent<Animator>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();

        Animator.SetFloat("yVelocity", Movement.Rigidbody.velocity.y);
        
        if (Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void ResetStunResistance()
    {
        _isStunned = false;
        _currentStunResistance = entityData.stunResistance;
    }
}

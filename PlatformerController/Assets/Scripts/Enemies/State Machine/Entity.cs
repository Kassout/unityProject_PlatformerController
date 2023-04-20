using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public EntityData entityData;

    public int FacingDirection { get; private set; }
    
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject AliveGameObject { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    
    private Vector2 _velocityWorkspace;

    public virtual void Start()
    {
        FacingDirection = 1;
        
        AliveGameObject = transform.Find("Alive").gameObject;
        Rigidbody = AliveGameObject.GetComponent<Rigidbody2D>();
        Animator = AliveGameObject.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
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

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGameObject.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * FacingDirection * entityData.wallCheckDistance);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * entityData.ledgeCheckDistance);
    }
}

using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;

    public float GroundCheckRadius => groundCheckRadius;
    public float WallCheckDistance => wallCheckDistance;

    public LayerMask WhatIsGround => whatIsGround;

    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }

    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }

    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }

    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    
    public bool Ceiling => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    public bool WallFront => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, 
            wallCheckDistance, whatIsGround);
    public bool WallBack => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, 
        wallCheckDistance, whatIsGround);
    public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, 
            wallCheckDistance, whatIsGround);
    public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down,
        wallCheckDistance, whatIsGround);
}

using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;

    public float GroundCheckRadius => groundCheckRadius;

    public float WallCheckDistance => wallCheckDistance;

    public LayerMask WhatIsGround => whatIsGround;

    public Transform GroundCheck => groundCheck;
    public Transform WallCheck => wallCheck;
    public Transform LedgeCheck => ledgeCheck;
    public Transform CeilingCheck => ceilingCheck;
    
    public bool Ceiling => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);

    public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    public bool WallFront => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, 
            wallCheckDistance, whatIsGround);
    
    public bool WallBack => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, 
        wallCheckDistance, whatIsGround);

    public bool Ledge => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, 
            wallCheckDistance, whatIsGround);
}

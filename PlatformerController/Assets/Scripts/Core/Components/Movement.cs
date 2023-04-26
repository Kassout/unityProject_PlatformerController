using UnityEngine;

public class Movement : CoreComponent
{
    public int FacingDirection { get; private set; }
    
    public Vector2 CurrentVelocity { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    
    private Vector2 _workspace;

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();
        
        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspace = direction * velocity;
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    
    private void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0f, 180f, 0f);
    }
    
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
}

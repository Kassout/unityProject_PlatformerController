using UnityEngine;

public class Movement : CoreComponent
{
    public bool CanSetVelocity { get; set; }
    
    public int FacingDirection { get; private set; }
    
    public Vector2 CurrentVelocity { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    
    private Vector2 _workspace;

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();
        
        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
    }

    public void SetVelocityZero()
    {
        _workspace = Vector2.zero;
        SetFinalVelocity();
    }
    
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspace = direction * velocity;
        SetFinalVelocity();
    }
    
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rigidbody.velocity = _workspace;
            CurrentVelocity = _workspace;
        }
    }
    
    public void Flip()
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

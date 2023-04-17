using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _movementInputDirection;

    private bool _isFacingRight = true;
    
    private Rigidbody2D _rigidbody;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckMovementDirection()
    {
        if (_isFacingRight && _movementInputDirection < 0)
        {
            Flip();
        }
        else if (!_isFacingRight && _movementInputDirection > 0)
        {
            Flip();
        }
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = new Vector2(movementSpeed * _movementInputDirection, _rigidbody.velocity.y);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
    }
}

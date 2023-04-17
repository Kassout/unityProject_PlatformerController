using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _movementInputDirection;
    private int _amountOfJumpsLeft;
    private bool _isFacingRight = true;
    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _canJump = false;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public int amountOfJumps = 1;
    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _amountOfJumpsLeft = amountOfJumps;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded && _rigidbody.velocity.y <= 0)
        {
            _amountOfJumpsLeft = amountOfJumps;
        }

        if (_amountOfJumpsLeft <= 0)
        {
            _canJump = false;
        }
        else
        {
            _canJump = true;
        }
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

        if (_rigidbody.velocity.x != 0)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("isWalking", _isWalking);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("yVelocity", _rigidbody.velocity.y);
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
        if (_canJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            _amountOfJumpsLeft--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

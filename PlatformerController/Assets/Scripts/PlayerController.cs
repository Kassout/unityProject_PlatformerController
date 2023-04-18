using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _movementInputDirection;
    private float _jumpTimer;
    private float _turnTimer;
    private float _wallJumpTimer;
    
    private int _amountOfJumpsLeft;
    private int _facingDirection = 1;
    private int _lastWallJumpDirection;
    
    private bool _isFacingRight = true;
    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _isTouchingWall = false;
    private bool _isWallSliding = false;
    private bool _canNormalJump = false;
    private bool _canWallJump = false;
    private bool _isAttemptingToJump = false;
    private bool _checkJumpMultiplier = false;
    private bool _canMove = false;
    private bool _canFlip = false;
    private bool _hasWallJumped = false;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public int amountOfJumps = 1;
    
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;
    
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    
    public LayerMask whatIsGround;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        if (_isTouchingWall && _movementInputDirection == _facingDirection && _rigidbody.velocity.y < 0.0f)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        _isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded && _rigidbody.velocity.y <= 0.01f)
        {
            _amountOfJumpsLeft = amountOfJumps;
        }

        if (_isTouchingWall)
        {
            _canWallJump = true;
        }

        if (_amountOfJumpsLeft <= 0)
        {
            _canNormalJump = false;
        }
        else
        {
            _canNormalJump = true;
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

        if (Mathf.Abs(_rigidbody.velocity.x) > 0.01f)
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
        _animator.SetBool("isWallSliding", _isWallSliding);
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded || (_amountOfJumpsLeft > 0 && _isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                _jumpTimer = jumpTimerSet;
                _isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && _isTouchingWall)
        {
            if (!_isGrounded && _movementInputDirection != _facingDirection)
            {
                _canMove = false;
                _canFlip = false;

                _turnTimer = turnTimerSet;
            }
        }

        if (!_canMove)
        {
            _turnTimer -= Time.deltaTime;

            if (_turnTimer <= 0)
            {
                _canMove = true;
                _canFlip = true;
            }
        }

        if (_checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            _checkJumpMultiplier = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * variableJumpHeightMultiplier);
        }
    }
    
    private void CheckJump()
    {
        if (_jumpTimer > 0)
        {
            // Wall Jump
            if (!_isGrounded && _isTouchingWall && _movementInputDirection != 0 && _movementInputDirection != _facingDirection)
            {
                WallJump();   
            }
            else if (_isGrounded)
            {
                NormalJump();
            }
        }
        
        if (_isAttemptingToJump)
        {
            _jumpTimer -= Time.deltaTime;
        }

        if (_wallJumpTimer > 0)
        {
            if (_hasWallJumped && _movementInputDirection == -_lastWallJumpDirection)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
                _hasWallJumped = false;
            }
            else if (_wallJumpTimer <= 0)
            {
                _hasWallJumped = false;
            }
            else
            {
                _wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (_canNormalJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            _amountOfJumpsLeft--;
            _jumpTimer = 0f;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if (_canWallJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
            _isWallSliding = false;
            _amountOfJumpsLeft = amountOfJumps;
            _amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * _movementInputDirection, wallJumpForce * wallJumpDirection.y);
            _rigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
            _jumpTimer = 0f;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
            _turnTimer = 0f;
            _canMove = true;
            _canFlip = true;
            _hasWallJumped = true;
            _wallJumpTimer = wallJumpTimerSet;
            _lastWallJumpDirection = -_facingDirection;
        }
    }

    private void ApplyMovement()
    {
        if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * airDragMultiplier, _rigidbody.velocity.y);
        }
        else if (_canMove)
        {
            _rigidbody.velocity = new Vector2(movementSpeed * _movementInputDirection, _rigidbody.velocity.y);
        }

        if (_isWallSliding)
        {
            if (_rigidbody.velocity.y < -wallSlideSpeed)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Flip()
    {
        if (!_isWallSliding && _canFlip)
        {
            _facingDirection *= -1;
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);   
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        var position = wallCheck.position;
        Gizmos.DrawLine(position, new Vector3(position.x + wallCheckDistance, position.y, position.z));
    }
}

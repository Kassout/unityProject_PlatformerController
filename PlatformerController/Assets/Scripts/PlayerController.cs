using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _movementInputDirection;
    private float _jumpTimer;
    private float _turnTimer;
    private float _wallJumpTimer;
    private float _dashTimeLeft;
    private float _lastImageXPosition;
    private float _lastDash = float.NegativeInfinity;
    
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
    private bool _isTouchingLedge = false;
    private bool _canClimbLedge = false;
    private bool _ledgeDetected = false;
    private bool _isDashing = false;

    private Vector2 _ledgePositionBottom;
    private Vector2 _ledgePosition1;
    private Vector2 _ledgePosition2;
    
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
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;
    
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
        CheckLedgeClimb();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        if (_isTouchingWall && _movementInputDirection == _facingDirection && _rigidbody.velocity.y < 0.0f && !_canClimbLedge)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void CheckLedgeClimb()
    {
        if (_ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;

            if (_isFacingRight)
            {
                _ledgePosition1 = new Vector2(Mathf.Floor(_ledgePositionBottom.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(_ledgePositionBottom.y) + ledgeClimbYOffset1);
                _ledgePosition2 = new Vector2(Mathf.Floor(_ledgePositionBottom.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(_ledgePositionBottom.y) + ledgeClimbYOffset2);
            }
            else
            {
                _ledgePosition1 = new Vector2(Mathf.Ceil(_ledgePositionBottom.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(_ledgePositionBottom.y) + ledgeClimbYOffset1);
                _ledgePosition2 = new Vector2(Mathf.Ceil(_ledgePositionBottom.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(_ledgePositionBottom.y) + ledgeClimbYOffset2);
            }

            _canMove = false;
            _canFlip = false;
            
            _animator.SetBool("canClimbLedge", _canClimbLedge);
        }

        if (_canClimbLedge)
        {
            transform.position = _ledgePosition1;
        }
    }

    private void FinishLedgeClimb()
    {
        _canClimbLedge = false;
        transform.position = _ledgePosition2;
        _canMove = true;
        _canFlip = true;
        _ledgeDetected = false;
        _animator.SetBool("canClimbLedge", _canClimbLedge);
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        _isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        _isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (_isTouchingWall && !_isTouchingLedge && !_ledgeDetected)
        {
            _ledgeDetected = true;
            _ledgePositionBottom = wallCheck.position;
        }
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded && _rigidbody.velocity.y <= 0.01f)
        {
            _amountOfJumpsLeft = amountOfJumps;
        }

        if (_isTouchingWall)
        {
            _checkJumpMultiplier = false;
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

        if (Mathf.Abs(_rigidbody.velocity.x) >= 0.01f)
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
            if (_isGrounded || (_amountOfJumpsLeft > 0 && !_isTouchingWall))
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

        if (_turnTimer >= 0)
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

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (_lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }

    private void AttemptToDash()
    {
        _isDashing = true;
        _dashTimeLeft = dashTime;
        _lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        _lastImageXPosition = transform.position.x;
    }

    private void CheckDash()
    {
        if (_isDashing)
        {
            if (_dashTimeLeft > 0)
            {
                _canMove = false;
                _canFlip = false;

                _rigidbody.velocity = new Vector2(dashSpeed * _facingDirection, 0.0f);
                _dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - _lastImageXPosition) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    _lastImageXPosition = transform.position.x;
                }
            }

            if (_dashTimeLeft <= 0 || _isTouchingWall)
            {
                _isDashing = false;
                _canMove = true;
                _canFlip = true;
            }
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

        var wallCheckPosition = wallCheck.position;
        Gizmos.DrawLine(wallCheckPosition, new Vector3(wallCheckPosition.x + wallCheckDistance, wallCheckPosition.y, wallCheckPosition.z));
        var ledgeCheckPosition = ledgeCheck.position;
        Gizmos.DrawLine(ledgeCheckPosition, new Vector3(ledgeCheckPosition.x + wallCheckDistance, ledgeCheckPosition.y, ledgeCheckPosition.z));
    }
}

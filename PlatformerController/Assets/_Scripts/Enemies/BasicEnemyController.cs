using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        KnockBack,
        Dead
    }

    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float touchDamageCooldown;
    [SerializeField] private float touchDamage;
    [SerializeField] private float touchDamageWidth;
    [SerializeField] private float touchDamageHeight;

    [SerializeField] private Vector2 knockBackSpeed;

    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform touchDamageCheck;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    private bool _groundDetected;
    private bool _wallDetected;

    private int _facingDirection = 1;
    private int _damageDirection;

    private float _currentHealth;
    private float _knockBackStartTime;
    private float _lastTouchDamageTime;

    private float[] _attackDetails = new float[2];

    private Vector2 _movement;
    private Vector2 _touchDamageBotLeft;
    private Vector2 _touchDamageTopRight;

    private State _currentState;

    private GameObject _aliveGameObject;

    private Rigidbody2D _aliveRigidbody;
    private Animator _aliveAnimator;

    private void Start()
    {
        _aliveGameObject = transform.Find("Alive").gameObject;
        _aliveRigidbody = _aliveGameObject.GetComponent<Rigidbody2D>();
        _aliveAnimator = _aliveGameObject.GetComponent<Animator>();

        _currentHealth = maxHealth;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.KnockBack:
                UpdateKnockBackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //--WALKING STATE---------------------------------------------------------------------------------------------------

    private void EnterMovingState()
    {
    }

    private void UpdateMovingState()
    {
        _groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        _wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!_groundDetected || _wallDetected)
        {
            Flip();
        }
        else
        {
            _movement.Set(movementSpeed * _facingDirection, _aliveRigidbody.velocity.y);
            _aliveRigidbody.velocity = _movement;
        }
    }

    private void ExitMovingState()
    {
    }

    //--KNOCK BACK STATE------------------------------------------------------------------------------------------------

    private void EnterKnockBackState()
    {
        _knockBackStartTime = Time.time;
        _movement.Set(knockBackSpeed.x * _damageDirection, knockBackSpeed.y);
        _aliveRigidbody.velocity = _movement;

        _aliveAnimator.SetBool("knockBack", true);
    }

    private void UpdateKnockBackState()
    {
        if (Time.time >= _knockBackStartTime + knockBackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockBackState()
    {
        _aliveAnimator.SetBool("knockBack", false);
    }

    //--DEAD STATE------------------------------------------------------------------------------------------------------

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, _aliveGameObject.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, _aliveGameObject.transform.position, deathBloodParticle.transform.rotation);

        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {
    }

    private void ExitDeadState()
    {
    }

    //--OTHER FUNCTIONS-------------------------------------------------------------------------------------------------

    private void Damage(float[] attackDetails)
    {
        _currentHealth -= attackDetails[0];

        Instantiate(hitParticle, _aliveGameObject.transform.position,
            Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > _aliveGameObject.transform.position.x)
        {
            _damageDirection = -1;
        }
        else
        {
            _damageDirection = 1;
        }

        // Hit Particle

        if (_currentHealth > 0.0f)
        {
            SwitchState(State.KnockBack);
        }
        else if (_currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= _lastTouchDamageTime + touchDamageCooldown)
        {
            var touchDamageCheckPosition = touchDamageCheck.position;
            _touchDamageBotLeft.Set(touchDamageCheckPosition.x - (touchDamageWidth / 2),
                touchDamageCheckPosition.y - (touchDamageHeight / 2));
            _touchDamageTopRight.Set(touchDamageCheckPosition.x + (touchDamageWidth / 2),
                touchDamageCheckPosition.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(_touchDamageBotLeft, _touchDamageTopRight, whatIsPlayer);
            if (hit)
            {
                _lastTouchDamageTime = Time.time;
                _attackDetails[0] = touchDamage;
                _attackDetails[1] = _aliveGameObject.transform.position.x;
                hit.SendMessage("Damage", _attackDetails);
            }
        }
    }

    private void Flip()
    {
        _facingDirection *= -1;
        _aliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (_currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.KnockBack:
                ExitKnockBackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.KnockBack:
                EnterKnockBackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        _currentState = state;
    }

    private void OnDrawGizmos()
    {
        var groundCheckPosition = groundCheck.position;
        Gizmos.DrawLine(groundCheckPosition,
            new Vector2(groundCheckPosition.x, groundCheckPosition.y - groundCheckDistance));

        var wallCheckPosition = wallCheck.position;
        Gizmos.DrawLine(wallCheckPosition, new Vector2(wallCheckPosition.x + wallCheckDistance, wallCheckPosition.y));

        var touchDamageCheckPosition = touchDamageCheck.position;
        var bottomLeft = new Vector2(touchDamageCheckPosition.x - (touchDamageWidth / 2),
            touchDamageCheckPosition.y - (touchDamageHeight / 2));
        var bottomRight = new Vector2(touchDamageCheckPosition.x + (touchDamageWidth / 2),
            touchDamageCheckPosition.y - (touchDamageHeight / 2));
        var topRight = new Vector2(touchDamageCheckPosition.x + (touchDamageWidth / 2),
            touchDamageCheckPosition.y + (touchDamageHeight / 2));
        var topLeft = new Vector2(touchDamageCheckPosition.x - (touchDamageWidth / 2),
            touchDamageCheckPosition.y + (touchDamageHeight / 2));
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
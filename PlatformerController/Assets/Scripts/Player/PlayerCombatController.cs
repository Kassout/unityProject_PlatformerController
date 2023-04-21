using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer;
    [SerializeField] private float attack1Radius;
    [SerializeField] private float attack1Damage;
    [SerializeField] private Transform attack1HitBoxPosition;
    [SerializeField] private LayerMask whatIsDamageable;

    private bool _gotInput = false;
    private bool _isAttacking = false;
    private bool _isFirstAttack = false;

    private float _lastInputTime = float.PositiveInfinity;

    private AttackDetails _attackDetails;

    private Animator _animator;
    private PlayerController _playerController;
    private PlayerStats _playerStats;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("canAttack", combatEnabled);
        
        _playerStats = GetComponent<PlayerStats>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                // Attempt combat
                _gotInput = true;
                _lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (_gotInput)
        {
            // Perform Attack1
            if (!_isAttacking)
            {
                _gotInput = false;
                _isAttacking = true;
                _isFirstAttack = !_isFirstAttack;
                _animator.SetBool("attack1", true);
                _animator.SetBool("firstAttack", _isFirstAttack);
                _animator.SetBool("isAttacking", _isAttacking);
            }
        }

        if (Time.time >= _lastInputTime + inputTimer)
        {
            // Wait for new input
            _gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPosition.position, attack1Radius,
            whatIsDamageable);

        _attackDetails.damageAmount = attack1Damage;
        _attackDetails.position = transform.position;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", _attackDetails);
            // Instantiate hit particles
        }
    }

    private void FinishAttack1()
    {
        _isAttacking = false;
        _animator.SetBool("isAttacking", _isAttacking);
        _animator.SetBool("attack1", false);
    }

    private void Damage(AttackDetails attackDetails)
    {
        if (!_playerController.GetDashStatus())
        {
            int direction;
        
            _playerStats.DecreaseHealth(attackDetails.damageAmount);
        
            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        
            _playerController.KnockBack(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPosition.position, attack1Radius);
    }
}
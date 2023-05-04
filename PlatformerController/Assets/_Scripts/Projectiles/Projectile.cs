using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePosition;
    
    private bool _isGravityOn;
    private bool _hasHitGround;
    
    private float _speed;
    private float _travelDistance;
    private float _damage;
    private float _xStartPosition;
    private float _stunDamage;
    private float _knockBackStrength;
    
    private Vector2 _knockBackAngle;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.gravityScale = 0f;
        _rigidbody.velocity = transform.right * _speed;

        _xStartPosition = transform.position.x;
    }

    private void Update()
    {
        if (!_hasHitGround)
        {
            if (_isGravityOn)
            {
                float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                IDamageable damageable = damageHit.GetComponent<IDamageable>();
                damageable?.Damage(_damage);
                
                IKnockBackable knockBackable = damageHit.GetComponent<IKnockBackable>();
                int facingDirection = (int)_rigidbody.velocity.normalized.x;
                knockBackable?.KnockBack(_knockBackAngle, _knockBackStrength, facingDirection);
                
                IStunnable stunnable = damageHit.GetComponent<IStunnable>();
                stunnable?.Stun(_stunDamage);
                Destroy(gameObject);
            }

            if (groundHit)
            {
                _hasHitGround = true;
                _rigidbody.gravityScale = 0f;
                _rigidbody.velocity = Vector2.zero;
            }
            
            if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance && !_isGravityOn)
            {
                _isGravityOn = true;
                _rigidbody.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage, float stunDamage, float knockBackStrength, Vector2 knockBackAngle)
    {
        _speed = speed;
        _travelDistance = travelDistance;
        _damage = damage;
        _stunDamage = stunDamage;
        _knockBackStrength = knockBackStrength;
        _knockBackAngle = knockBackAngle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}

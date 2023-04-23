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
    private float _xStartPosition;

    private AttackDetails _attackDetails;

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
            _attackDetails.position = transform.position;
            
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
                damageHit.transform.SendMessage("Damage", _attackDetails);
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

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        _speed = speed;
        _travelDistance = travelDistance;
        _attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}

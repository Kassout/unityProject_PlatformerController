using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    private readonly List<IDamageable> _detectedDamageable = new List<IDamageable>();
    private List<IKnockBackable> _detectedKnockBackable = new List<IKnockBackable>();
    private List<IStunnable> _detectedStunnable = new List<IStunnable>();

    protected AggressiveWeaponData _aggressiveWeaponData;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(AggressiveWeaponData))
        {
            _aggressiveWeaponData = (AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon.");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = _aggressiveWeaponData.AttackDetails[_attackCounter];
        
        foreach (var damageable in _detectedDamageable.ToList())
        {
            damageable.Damage(details.damageAmount);
        }

        foreach (var knockBackable in _detectedKnockBackable.ToList())
        {
            knockBackable.KnockBack(details.knockBackAngle, details.knockBackStrength, Movement.FacingDirection);
        }

        foreach (var stunnable in _detectedStunnable.ToList())
        {
            stunnable.Stun(details.stunAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        
        if (damageable is { Damageable: true })
        {
            _detectedDamageable.Add(damageable);
        }

        IKnockBackable knockBackable = collision.GetComponent<IKnockBackable>();

        if (knockBackable is { KnockBackable: true })
        {
            _detectedKnockBackable.Add(knockBackable);
        }

        IStunnable stunnable = collision.GetComponent<IStunnable>();

        if (stunnable is { Stunnable: true })
        {
            _detectedStunnable.Add(stunnable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            _detectedDamageable.Remove(damageable);
        }
        
        IKnockBackable knockBackable = collision.GetComponent<IKnockBackable>();

        if (knockBackable != null)
        {
            _detectedKnockBackable.Remove(knockBackable);
        }

        IStunnable stunnable = collision.GetComponent<IStunnable>();

        if (stunnable != null)
        {
            _detectedStunnable.Remove(stunnable);
        }
    }
}

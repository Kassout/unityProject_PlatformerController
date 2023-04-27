using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    private readonly List<IDamageable> _detectedDamageable = new List<IDamageable>();
    private List<IKnockBackable> _detectedKnockBackable = new List<IKnockBackable>();

    protected AggressiveWeaponData _aggressiveWeaponData;

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
            knockBackable.KnockBack(details.knockBackAngle, details.knockBackStrength, _core.Movement.FacingDirection);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            _detectedDamageable.Add(damageable);
        }

        IKnockBackable knockBackable = collision.GetComponentInChildren<IKnockBackable>();

        if (knockBackable != null)
        {
            _detectedKnockBackable.Add(knockBackable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            _detectedDamageable.Remove(damageable);
        }
        
        IKnockBackable knockBackable = collision.GetComponentInChildren<IKnockBackable>();

        if (knockBackable != null)
        {
            _detectedKnockBackable.Remove(knockBackable);
        }
    }
}

using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockBackable, IStunnable
{
    #region Fields

    [SerializeField] private float maxKnockBackTime = 0.2f;
    
    [SerializeField] private GameObject damageParticles;
    
    private bool _isKnockBackActive;
    
    private float _knockBackStartTime;
    
    private Stats _stats;
    private Movement _movement;
    private CollisionSenses _collisionSenses;
    private ParticleManager _particleManager;

    #endregion

    #region Properties

    private Stats Stats => _stats ? _stats : _core.GetCoreComponent(out _stats);
    private Movement Movement => _movement ? _movement : _core.GetCoreComponent(out _movement);
    private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : _core.GetCoreComponent(out _collisionSenses);
    private ParticleManager ParticleManager => _particleManager ? _particleManager : _core.GetCoreComponent(out _particleManager);

    #endregion

    #region CoreComponent

    public override void LogicUpdate()
    {
        CheckKnockBack();
    }

    #endregion

    #region IDamageable
    
    [SerializeField] private bool damageable;
    
    public bool Damageable => damageable;

    public void Damage(float amount)
    {
        Stats.DecreaseHealth(amount);
        ParticleManager.StartParticlesWithRandomRotation(damageParticles);
    }
    
    #endregion
    
    #region IKnockBackable

    [SerializeField] private bool knockBackable;

    public bool KnockBackable => knockBackable;

    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        Movement.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        _isKnockBackActive = true;
        _knockBackStartTime = Time.time;
    }
    
    #endregion
    
    #region IStunnable
    
    [SerializeField] private bool stunnable;

    public bool Stunnable => stunnable;
    
    public void Stun(float amount)
    {
        Stats.DecreaseStunResistance(amount);
    }

    #endregion

    #region Private

    private void CheckKnockBack()
    {
        if (_isKnockBackActive && ((Movement.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground) || Time.time >= _knockBackStartTime + maxKnockBackTime))
        {
            _isKnockBackActive = false;
            Movement.CanSetVelocity = true;
        }
    }

    #endregion
}

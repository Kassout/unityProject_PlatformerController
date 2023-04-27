using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockBackable
{
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private CollisionSenses CollisionSenses => _collisionSenses ??= _core.GetCoreComponent<CollisionSenses>();
    private Stats Stats => _stats ??= _core.GetCoreComponent<Stats>();

    private Movement _movement;
    private CollisionSenses _collisionSenses;
    private Stats _stats;
    
    [SerializeField] private float maxKnockBackTime = 0.2f;

    private bool _isKnockBackActive;
    private float _knockBackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockBack();
    }
    
    public void Damage(float amount)
    {
        Stats.DecreaseHealth(amount);
    }

    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        Movement.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        _isKnockBackActive = true;
        _knockBackStartTime = Time.time;
    }

    private void CheckKnockBack()
    {
        if (_isKnockBackActive && ((Movement.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground) || Time.time >= _knockBackStartTime + maxKnockBackTime))
        {
            _isKnockBackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}

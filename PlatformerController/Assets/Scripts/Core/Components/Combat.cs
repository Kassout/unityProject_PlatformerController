using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockBackable
{
    [SerializeField] private float maxKnockBackTime = 0.2f;

    private bool _isKnockBackActive;
    private float _knockBackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockBack();
    }
    
    public void Damage(float amount)
    {
        core.Stats.DecreaseHealth(amount);
    }

    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;
        _isKnockBackActive = true;
        _knockBackStartTime = Time.time;
    }

    private void CheckKnockBack()
    {
        if (_isKnockBackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground) || Time.time >= _knockBackStartTime + maxKnockBackTime))
        {
            _isKnockBackActive = false;
            core.Movement.CanSetVelocity = true;
        }
    }
}

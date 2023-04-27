using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockBackable
{
    private bool _isKnockBackActive;
    private float _knockBackStartTime;

    public void LogicUpdate()
    {
        CheckKnockBack();
    }
    
    public void Damage(float damage)
    {
        Debug.Log(core.transform.parent.name + " got hit.");
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
        if (_isKnockBackActive && core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground)
        {
            _isKnockBackActive = false;
            core.Movement.CanSetVelocity = true;
        }
    }
}

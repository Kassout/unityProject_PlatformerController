using UnityEngine;

public class RangedAttackState : AttackState
{
    protected RangedAttackStateData _stateData;

    protected GameObject _projectile;
    protected Projectile _projectileScript;
    
    public RangedAttackState(Entity entity, string animationBoolName, 
        Transform attackPosition, RangedAttackStateData stateData) : base(entity, animationBoolName, attackPosition)
    {
        _stateData = stateData;
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        _projectile = Object.Instantiate(_stateData.projectile, _attackPosition.position, _attackPosition.rotation);
        _projectileScript = _projectile.GetComponent<Projectile>();
        _projectileScript.FireProjectile(_stateData.projectileSpeed, _stateData.projectileTravelDistance, _stateData.projectileDamage, _stateData.stunDamage, _stateData.knockBackStrength, _stateData.knockBackAngle);
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}

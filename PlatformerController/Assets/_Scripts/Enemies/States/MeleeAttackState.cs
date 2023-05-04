using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData _stateData;
    
    private Movement Movement => _movement ??= _core.GetCoreComponent<Movement>();
    private Movement _movement;
    
    public MeleeAttackState(Entity entity, string animationBoolName, 
        Transform attackPosition, MeleeAttackStateData stateData) 
        : base(entity, animationBoolName, attackPosition)
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

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_attackPosition.position, 
            _stateData.attackRadius, _stateData.whatIsPlayer);

        for (int i = 0; i < detectedObjects.Length; i++)
        {
            IDamageable damageable = detectedObjects[i].GetComponent<IDamageable>();
            damageable?.Damage(_stateData.attackDamage);

            IKnockBackable knockBackable = detectedObjects[i].GetComponent<IKnockBackable>();
            knockBackable?.KnockBack(_stateData.knockBackAngle, _stateData.knockBackStrength, Movement.FacingDirection);

            IStunnable stunnable = detectedObjects[i].GetComponent<IStunnable>();
            stunnable?.Stun(_stateData.stunDamage);
        }
    }
}

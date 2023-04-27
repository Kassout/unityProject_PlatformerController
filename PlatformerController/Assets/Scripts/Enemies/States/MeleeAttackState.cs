using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected MeleeAttackStateData _stateData;

    protected AttackDetails _attackDetails;
    
    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        Transform attackPosition, MeleeAttackStateData stateData) 
        : base(entity, stateMachine, animationBoolName, attackPosition)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _attackDetails.damageAmount = _stateData.attackDamage;
        _attackDetails.position = _entity.transform.position;
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
            detectedObjects[i].transform.SendMessage("Damage", _attackDetails);
        }
    }
}

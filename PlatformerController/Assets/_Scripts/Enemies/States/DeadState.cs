using UnityEngine;

public class DeadState : State
{
    protected DeadStateData _stateData;
    
    public DeadState(Entity entity, string animationBoolName, DeadStateData stateData) 
        : base(entity, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Object.Instantiate(_stateData.deathBloodParticles, _entity.transform.position, _stateData.deathBloodParticles.transform.rotation);
        Object.Instantiate(_stateData.deathChunkParticles, _entity.transform.position, _stateData.deathChunkParticles.transform.rotation);

        _entity.gameObject.SetActive(false);
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
}

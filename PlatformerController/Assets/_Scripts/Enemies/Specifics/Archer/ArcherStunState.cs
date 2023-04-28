public class ArcherStunState : StunState
{
    private Archer _archer;
    
    public ArcherStunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        StunStateData stateData, Archer archer) 
        : base(entity, stateMachine, animationBoolName, stateData)
    {
        _archer = archer;
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

        if (_isStunTimeOver)
        {
            if (_isPlayerInMinAggroRange)
            {
                _stateMachine.ChangeState(_archer.PlayerDetectedState);
            }
            else
            {
                _stateMachine.ChangeState(_archer.LookForPlayerState);
            }
        }
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

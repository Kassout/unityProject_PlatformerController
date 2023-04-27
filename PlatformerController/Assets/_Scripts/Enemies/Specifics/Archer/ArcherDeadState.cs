public class ArcherDeadState : DeadState
{
    private Archer _archer;

    public ArcherDeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, 
        DeadStateData stateData, Archer archer) 
        : base(entity, stateMachine, animationBoolName, stateData)
    {
        _archer = archer;
    }
}

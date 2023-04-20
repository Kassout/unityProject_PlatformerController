using UnityEngine;

public class HellDog : Entity
{
    public HellDogIdleState IdleState { get; private set; }
    public HellDogMoveState MoveState { get; private set; }
    public HellDogPlayerDetectedState PlayerDetectedState { get; private set; }

    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private PlayerDetectedStateData playerDetectedStateData;

    public override void Start()
    {
        base.Start();

        MoveState = new HellDogMoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new HellDogIdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new HellDogPlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);

        stateMachine.Initialize(MoveState);
    }
}

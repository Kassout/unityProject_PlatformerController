using UnityEngine;

public class HellDog : Entity
{
    public HellDogIdleState IdleState { get; private set; }
    public HellDogMoveState MoveState { get; private set; }

    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private MoveStateData moveStateData;

    public override void Start()
    {
        base.Start();

        MoveState = new HellDogMoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new HellDogIdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(MoveState);
    }
}

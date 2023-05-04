using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    
    [SerializeField] private PlayerData playerData;
    
    public PlayerData PlayerData => playerData;
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    
    #endregion
    
    #region Components
    
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    #endregion

    #region Other Variables

    private Vector2 _rigidbodyWorkspace;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, "idle");
        MoveState = new PlayerMoveState(this, "move");
        JumpState = new PlayerJumpState(this, "inAir");
        InAirState = new PlayerInAirState(this, "inAir");
        LandState = new PlayerLandState(this, "land");
        WallSlideState = new PlayerWallSlideState(this, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, "ledgeClimbState");
        DashState = new PlayerDashState(this, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, "attack");
        SecondaryAttackState = new PlayerAttackState(this, "attack");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<PlayerInventory>();
        DashDirectionIndicator = transform.Find("Dash Direction Indicator");

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.Primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.Primary]);
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        _rigidbodyWorkspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;
        
        MovementCollider.size = _rigidbodyWorkspace;
        MovementCollider.offset = center;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
}

using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    
    [SerializeField] private PlayerData playerData;
    
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    #endregion
    
    #region Components

    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    #endregion

    #region Other Variables

    private Vector2 _rigidbodyWorkspace;
    
    public int FacingDirection { get; private set; }
    
    public Vector2 CurrentVelocity { get; private set; }
    
    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();

        FacingDirection = 1;
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Getters / Setters

    public void SetVelocityX(float velocity)
    {
        _rigidbodyWorkspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _rigidbodyWorkspace;
        CurrentVelocity = _rigidbodyWorkspace;
    }

    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    #endregion
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CombatInputs
{
    Primary,
    Secondary
}

public class PlayerInputHandler : MonoBehaviour
{
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    
    public bool[] AttackInputs { get; private set; }
    
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    
    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    
    [SerializeField] private float inputHoldTime = 0.2f;

    private float _jumpInputStartTime;
    private float _dashInputStartTime;

    private PlayerInput _playerInput;
    private Camera _camera;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _camera = Camera.main;

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormalizedInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _dashInputStartTime = Time.time;
        } 
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirection(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (_playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = _camera.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.Primary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.Primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.Secondary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.Secondary] = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= _dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}

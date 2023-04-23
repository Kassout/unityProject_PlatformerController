using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _movementInput;

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();

        Debug.Log(_movementInput.ToString());
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump button pushed down now");
        }

        if (context.performed)
        {
            Debug.Log("Jump is being held down");
        }

        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }
    }
}

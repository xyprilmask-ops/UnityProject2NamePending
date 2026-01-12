using UnityEngine;
using UnityEngine.InputSystem; // For new Input System

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        // Check for keyboard input
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveInput.x = -1;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveInput.x = 1;
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveInput.y = 1;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveInput.y = -1;
        }

        // Check for gamepad input (left stick)
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            moveInput.x = gamepad.leftStick.x.ReadValue(); // Analog value (-1 to 1)
            moveInput.y = gamepad.leftStick.y.ReadValue();
        }

        // Calculate movement vector
        Vector2 movement = moveInput.normalized * speed * Time.deltaTime;

        // Apply movement
        transform.Translate(movement);
    }
}

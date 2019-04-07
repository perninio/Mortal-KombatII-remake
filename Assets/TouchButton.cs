using UnityEngine;
using UnityEngine.UI;

public class TouchButton : MonoBehaviour
{
    bool pressedDown;
    bool pressedLastFrame;

    public InputManager.ButtonState CurrentState;

    public void PressDown()
    {
        pressedDown = true;
    }

    public void Release()
    {
        pressedDown = false;
    }

    void Update()
    {
        // update the state based on the change since the last frame

        // update the state when the button is pressed
        if (pressedDown)
        {
            if (pressedLastFrame)
            {
                // was pressed in the previous frame and is still pressed, so the button is considered held
                CurrentState = InputManager.ButtonState.Held;
            }
            else
            {
                // button not pressed last frame, but is now, so the button has been pressed down on this frame
                CurrentState = InputManager.ButtonState.PressedDown;
            }
        }
        else
        {
            // now update if the button is not pressed
            if (pressedLastFrame)
            {
                // was pressed last frame, but no longer pressed, so it was released
                CurrentState = InputManager.ButtonState.Released;
            }
            else
            {
                // was not pressed last frame and still not pressed, so nothing
                CurrentState = InputManager.ButtonState.None;
            }
        }
    }

    private void LateUpdate()
    {
        // store the state from the last frame so it can be compared to the next frame's state to check if it has changed
        pressedLastFrame = pressedDown;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum ButtonState
    {
        None,
        PressedDown,
        Released,
        Held
    }

    //[SerializeField] TouchButton upButton;
   // [SerializeField] TouchButton downButton;
    [SerializeField] TouchButton leftButton;
    [SerializeField] TouchButton rightButton;


    public Vector2 CurrentInput
    {
        get
        {
            return new Vector2(HorizontalInput, Input.GetAxis("Vertical"));//, VerticalInput);
        }
    }

    float HorizontalInput
    {
        get
        {
            if (leftButton.CurrentState == ButtonState.Held || leftButton.CurrentState == ButtonState.PressedDown)
            {
                return -1;
            }
            else if (rightButton.CurrentState == ButtonState.Held || rightButton.CurrentState == ButtonState.PressedDown)
            {
                return 1;
            }
            return Input.GetAxis("Horizontal");
        }
    }
    /*
    float VerticalInput
    {
        get
        {
            if (upButton.CurrentState == ButtonState.Held || upButton.CurrentState == ButtonState.PressedDown)
            {
                return 1;
            }
            else if (downButton.CurrentState == ButtonState.Held || downButton.CurrentState == ButtonState.PressedDown)
            {
                return -1;
            }
            return Input.GetAxis("Vertical");
        }
    }
    */
}
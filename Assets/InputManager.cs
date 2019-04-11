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


    [SerializeField] TouchButton leftButton;
    [SerializeField] TouchButton rightButton;
    [SerializeField] TouchButton upButton;
    [SerializeField] TouchButton downButton;
    [SerializeField] TouchButton kickButton;
    [SerializeField] TouchButton punchButton;
    [SerializeField] TouchButton blockButton;

    public bool rightside = true;
    public bool btnrealeased = true;
    public bool ismoving=false;
    public bool isblocking = false;//do zaimplementowania
    public bool ispunching = false;//do zaimplementowania
    public bool iskicking = false;//do zaimplementownia
    public bool iskneeling = false;
    public bool isjumping = false;
    public float randNumber=0;


    public Vector2 CurrentInput
    {
        get
        {
            return new Vector2(HorizontalInput, VerticalInput);//, VerticalInput);
        }
    }

    float HorizontalInput
    {
        get
        {
            if (leftButton.CurrentState == ButtonState.Held || leftButton.CurrentState == ButtonState.PressedDown)
            {
                ismoving = true;
                if (btnrealeased && rightside)
                    Flip();
                return -1;
            }

            else if (rightButton.CurrentState == ButtonState.Held || rightButton.CurrentState == ButtonState.PressedDown)
            {
                ismoving = true;
                if (btnrealeased && !rightside)
                    Flip();
                return 1;
            }
            else if (rightButton.CurrentState == ButtonState.None || leftButton.CurrentState == ButtonState.None || rightButton.CurrentState == ButtonState.Released || leftButton.CurrentState == ButtonState.Released)
            {
                btnrealeased = true;
                ismoving = false;
                return 0;
            }
            return Input.GetAxis("Horizontal");
                
        }


    }
    //Flip changes the x to -x so it reverse the object
    void Flip()
    {
        btnrealeased = false;
        rightside = !rightside;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    


    float VerticalInput
    {
        get
        {
            if (upButton.CurrentState == ButtonState.Held || upButton.CurrentState == ButtonState.PressedDown)
            {
                iskneeling = false;
                isjumping = true;
                return 1;
            }
            else if (downButton.CurrentState == ButtonState.Held || downButton.CurrentState == ButtonState.PressedDown)
            {
                iskneeling = true;
                return 0;
            }
            else if (downButton.CurrentState == ButtonState.None ||downButton.CurrentState == ButtonState.Released|| upButton.CurrentState == ButtonState.Released || upButton.CurrentState == ButtonState.None )
            {
                iskneeling = false;
                isjumping = false;
            }
            return Input.GetAxis("Vertical");
        }
    }
    public void Update()
    {
       if (blockButton.CurrentState == ButtonState.Held || blockButton.CurrentState == ButtonState.PressedDown)
        {
            isblocking = true;
        }
       else if (blockButton.CurrentState == ButtonState.Released  || blockButton.CurrentState == ButtonState.None)
        {
            isblocking = false;
        }
       if (kickButton.CurrentState == ButtonState.Held ||kickButton.CurrentState == ButtonState.PressedDown)
        {
            iskicking = true;
        }
        else if (kickButton.CurrentState == ButtonState.Released || kickButton.CurrentState == ButtonState.None)
        {
            iskicking = false;
        }
        if (punchButton.CurrentState == ButtonState.Held || punchButton.CurrentState == ButtonState.PressedDown)
        {
            ispunching = true;
        }
        else if (punchButton.CurrentState == ButtonState.Released || punchButton.CurrentState == ButtonState.None)
        {
            ispunching = false;
            randNumber=Random.Range(0f,1f);
        }
    }
}
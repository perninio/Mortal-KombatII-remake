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
    public bool isGrounded = true;
    public float randNumber=0;

    public static bool finishpunchbool = false;

    // Attack pos and range
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    // Kick speed
    public float KickRate = 0.8f;
    public float NextKick;

    // Punch speed
    public float PunchRate = 0.7f;
    public float NextPunch;

    // Moves list
    List<char> currentCombo = new List<char>();

    public Vector2 CurrentInput
    {
        get
        {
            return new Vector2(HorizontalInput, VerticalInput);
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
            if (upButton.CurrentState == ButtonState.PressedDown)
            {
                iskneeling = false;
                return 1;
            }
            else if (downButton.CurrentState == ButtonState.Held || downButton.CurrentState == ButtonState.PressedDown)
            {
                iskneeling = true;
                return 0;
            }
            else if (downButton.CurrentState == ButtonState.None || downButton.CurrentState == ButtonState.Released)
            {
                iskneeling = false;
            }
            return Input.GetAxis("Vertical");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            isjumping = false;
            Debug.Log("Grounded");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
            isjumping = true;
            Debug.Log("Grounded");
        }
    }


    public void Update()
    {
        //Blocking
        if (blockButton.CurrentState == ButtonState.Held || blockButton.CurrentState == ButtonState.PressedDown)
        {
            isblocking = true;
        }
        else if (blockButton.CurrentState == ButtonState.Released || blockButton.CurrentState == ButtonState.None)
        {
            isblocking = false;
        }

       //Kicking
       if (kickButton.CurrentState == ButtonState.PressedDown && Time.time > NextKick)
        {

            iskicking = true;
            NextKick = Time.time + KickRate;
            Collider2D[] enemieshit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            for (int i = 0; i < enemieshit.Length; i++)
            {
                enemieshit[i].GetComponent<Bot>().TakeDamage(15);
                Debug.Log("Kick");
            }
        }
        else if (kickButton.CurrentState == ButtonState.Released || kickButton.CurrentState == ButtonState.Held || kickButton.CurrentState == ButtonState.None)
        {
            iskicking = false;
        }

        // Punching
        // 4 Ciosy kombo w trakcie kombo można blokować
        // Po 4 ciosach przeciwnik odlatuje
        // Losujemy tylko pierwszy cios reszta jest naprzemian
        if (punchButton.CurrentState == ButtonState.PressedDown && Time.time > NextPunch)
        {
            if(Endgame.enableFinisher)
            {
                finishpunchbool = true;
            }
            ispunching = true;
            NextPunch = Time.time + PunchRate;
            Collider2D[] enemieshit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            for (int i = 0; i < enemieshit.Length; i++) {
                enemieshit[i].GetComponent<Bot>().TakeDamage(10);
                Debug.Log("Punch");
            }

        }
        else if (punchButton.CurrentState == ButtonState.Released || punchButton.CurrentState == ButtonState.Held || punchButton.CurrentState == ButtonState.None)
        {
            ispunching = false;
            randNumber=Random.Range(0f, 1f);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
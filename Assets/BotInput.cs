using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotInput : MonoBehaviour
{
    public enum ButtonState
    {
        None,
        PressedDown,
        Released,
        Held
    }

    public bool rightside = true;
    public bool btnrealeased = true;
    public bool ismoving = false;
    public bool isblocking = false;//do zaimplementowania
    public bool ispunching = false;//do zaimplementowania
    public bool iskicking = false;//do zaimplementownia
    public bool iskneeling = false;
    public bool isjumping = false;
    public bool isGrounded = true;
    public float randNumber = 0;

    // Attack pos and range
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    // Kick speed
    public float KickRate = 0.8f;
    public float NextKick;

    // Punch speed



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
            return 0;
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
            return 0;
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
        //if (blockButton.CurrentState == ButtonState.Held || blockButton.CurrentState == ButtonState.PressedDown)
        //{
        //    isblocking = true;
        //}
        //else if (blockButton.CurrentState == ButtonState.Released || blockButton.CurrentState == ButtonState.None)
        //{
        //    isblocking = false;
        //}

        //Kicking
        //if (kickButton.CurrentState == ButtonState.PressedDown && Time.time > NextKick)
        //{
        //    iskicking = true;
        //    NextKick = Time.time + KickRate;
        //    Collider2D[] enemieshit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        //    for (int i = 0; i < enemieshit.Length; i++)
        //    {
        //        Debug.Log("HIT");
        //    }
        //}
        //else if (kickButton.CurrentState == ButtonState.Released || kickButton.CurrentState == ButtonState.Held || kickButton.CurrentState == ButtonState.None)
        //{
        //    iskicking = false;
        //}

        // Punching
        // 4 Ciosy kombo w trakcie kombo można blokować
        // Po 4 ciosach przeciwnik odlatuje
        // Losujemy tylko pierwszy cios reszta jest naprzemian
        //    if (punchButton.CurrentState == ButtonState.PressedDown)
        //    {
        //        ispunching = true;
        //        Collider2D[] enemieshit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        //        for (int i = 0; i < enemieshit.Length; i++)
        //        {
        //            Debug.Log("HIT");
        //        }

        //    }
        //    else if (punchButton.CurrentState == ButtonState.Released || punchButton.CurrentState == ButtonState.Held || punchButton.CurrentState == ButtonState.None)
        //    {
        //        ispunching = false;
        //        randNumber = Random.Range(0f, 1f);
        //    }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
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



    // Decision making
    float kickRatio = 0.5f;
    float punchRatio = 0.4f;
    float blockRatio = 0.1f;
    public float blockRate = 1.5f;
    public float kickRate = 1.2f;
    public float punchRate = 1f;
    public float nextMove = 0f;

    Bot bot;
    private void Awake()
    {
        bot = GetComponent<Bot>();
    }

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

    public void defensiveState()
    {
        kickRatio = 0.3f;
        punchRatio = 0.3f;
        blockRatio = 0.4f;
    }

    public void makeMove() {
        float brain = Random.Range(0f, 1f);
        if (brain <= kickRatio)
        {
            // Przemek dodaj to kopanie dla sub Z i odkometuj sprwadzacz trafienia
            Debug.Log("That is a fucking kick");
            iskicking = true;
            //checkIfHit(15);
            nextMove = Time.time + kickRate;
        }
        else if (brain > kickRatio && brain <= punchRatio + kickRatio)
        {
            Debug.Log("That is a fucking punch");
            ispunching = true;
            checkIfHit(10);
            nextMove = Time.time + punchRate;
        }
        else {
            Debug.Log("Screw it BLOCK that shit");
            isblocking = true;
            nextMove = Time.time + blockRate;
        }
    }

    public void checkIfHit(int damage) {
        Collider2D[] enemieshit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemieshit.Length; i++)
        {
            enemieshit[i].GetComponent<Player>().TakeDamage(damage);
            Debug.Log("Bot hit player");
        }
    }

    public void Update() {

        if (bot.moveleft)
        {
            ismoving = true;
            isblocking = false;
        }
        else if (bot.moveRight)
        {
            ismoving = true;
            isblocking = false;
        }
        else
        {
            ismoving = false;
            iskicking = false;
            ispunching = false;
            if (nextMove < Time.time)
            {
                isblocking = false;
                makeMove();
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
     }
}
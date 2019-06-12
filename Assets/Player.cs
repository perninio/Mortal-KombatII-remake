using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour

{
    InputManager inputManager;
    Animator animationController;

   [SerializeField] float playerSpeed = 5f;    
   [SerializeField] public GameObject canvaseControls;
   [SerializeField] public GameObject healthBar;
    public GameObject location;
    public float jumpHeight = 7f;
    public Rigidbody2D rb;
    public int health;
    public Animator fire;
    private Animator animDamage;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Awake Player");
        health = 100;
        animDamage = GetComponent<Animator>();
        Endgame.player = this;
    }

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animationController = GetComponent<Animator>();
        GameObject bot = GameObject.FindGameObjectWithTag("Enemy");
        Bot botscript = bot.GetComponent<Bot>();
        botscript.target = this.transform;
    }

    public void TakeDamage(int damage)
    {
        if (inputManager.isblocking)
        {
            damage = 1;
            Debug.Log("HIT Blocked ! " + damage);
        }
        if (!inputManager.isblocking)
        {
            animDamage.Play("takehit", 0, 0);
        }
        health -= damage;
        // TO DO blood effect
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Debug.Log("damage TAKEN ! " + damage);
        healthBar.GetComponent<HealthBar>().SetSize((float)health / 100);
    }

    void Update()
    {
        if (MainMenu.canvas)
        {
            MainMenu.canvas = false;
            canvaseControls.SetActive(true);
        }        
         if(InputManager.finishpunchbool)
        {
            animationController.SetTrigger("finishpunch");
            location.transform.position = GameObject.Find("bot").transform.position;
            location.SetActive(true);
            fire.SetBool("onfire",true);
            InputManager.finishpunchbool = false;
        }

        animationController.SetBool("ismoving", inputManager.ismoving);
        animationController.SetBool("iskneeling", inputManager.iskneeling);
        animationController.SetBool("isjumping", inputManager.isjumping);
        animationController.SetBool("isblocking", inputManager.isblocking);
        animationController.SetBool("iskicking", inputManager.iskicking);
        animationController.SetBool("ispunching", inputManager.ispunching);
        animationController.SetFloat("punch1", inputManager.randNumber);
        whenactionstopmoving();

        if (inputManager.CurrentInput.x != 0.0)
        {
            transform.Translate(inputManager.CurrentInput * Time.deltaTime * playerSpeed);
        }
        else if (inputManager.CurrentInput.y != 0.0 && !inputManager.isjumping)
        {
            Debug.Log("Jumping from player");

            Debug.Log(rb.velocity);
            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            rb.velocity = new Vector2(0.0f, jumpHeight);
        }
        //else if (inputManager.isjumping)
        //{
        //    rb.velocity = new Vector2(0.0f, -50f * Time.deltaTime);
        //}
       // Debug.Log(inputManager.CurrentInput);
    }

    private void whenactionstopmoving()
    {
        if (inputManager.iskneeling || inputManager.iskicking || inputManager.ispunching)
            playerSpeed = 0;
        else if (inputManager.isjumping)
            playerSpeed = 4;
        else
            playerSpeed = 5;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bot : MonoBehaviour

{
    BotInput inputManager;
    Animator animationController;
    [SerializeField] public GameObject healthBar;
    [SerializeField] float playerSpeed = 5f;
    public GameObject bloodEffect;
    public float jumpHeight = 10f;
    public Rigidbody2D rb;
    private int health = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        inputManager = GetComponent<BotInput>();
        animationController = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        health -= damage;
        // TO DO blood effect
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Debug.Log("damage TAKEN ! " + damage);
        healthBar.GetComponent<HealthBar>().SetSize((float)health/100);

    }

    void Update()
    {

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
            rb.velocity = new Vector2(0.0f, jumpHeight);
        }
    }

    private void whenactionstopmoving()
    {
        if (inputManager.iskneeling == true || inputManager.iskicking == true || inputManager.ispunching == true)
            playerSpeed = 0;
        else
            playerSpeed = 5;
    }
}
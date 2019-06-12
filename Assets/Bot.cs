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
    public int health;

    // Bot AI
    public Transform target;
    public LayerMask enemy;
    public bool moveleft = false;
    public bool moveRight = false;
    public bool canAttack = false;


    // Block action rate
    public float BlockRate = 5f;
    public float NextBlock;
    private Animator animDamage;
    private void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        animDamage = GetComponent<Animator>();
    }

    private void Awake()
    {
        inputManager = GetComponent<BotInput>();
        animationController = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        if (inputManager.isblocking) {
            damage = 1;
            Debug.Log("HIT Blocked ! " + damage);
        }
        if (!inputManager.isblocking) {
            animDamage.Play("takehit", 0, 0);
        }
        health -= damage;
        if (health < 50) {
            inputManager.defensiveState();
        }
        // TO DO blood effect
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Debug.Log("damage TAKEN ! " + damage);
        healthBar.GetComponent<HealthBar>().SetSize((float)health/100);
    }

    void Update()
    {
        if (!animationController.GetBool("stunt"))
        {
            animationController.SetBool("ismoving", inputManager.ismoving);
            animationController.SetBool("iskneeling", inputManager.iskneeling);
            animationController.SetBool("isjumping", inputManager.isjumping);
            animationController.SetBool("iskicking", inputManager.iskicking);
            animationController.SetBool("ispunching", inputManager.ispunching);
            animationController.SetBool("isblocking", inputManager.isblocking);
            animationController.SetFloat("punch1", inputManager.randNumber);
            whenactionstopmoving();

            // Move part
            RaycastHit hit;
            Vector3 left = transform.TransformDirection(Vector3.left) * 15;
            Ray lefrRay = new Ray(transform.position, left);

            Vector3 right = transform.TransformDirection(Vector3.right) * 15;
            Ray righRay = new Ray(transform.position, right);

            Debug.DrawRay(transform.position, left, Color.green);
            Debug.DrawRay(transform.position, right, Color.green);

            // Cast a ray.
            if (Physics.Raycast(lefrRay, out hit))
            {
                if (hit.distance > 1.35f)
                {
                    moveleft = true;
                }
                else
                {
                    moveleft = false;
                    canAttack = true;
                }
            }
            else if (Physics.Raycast(righRay, out hit))
            {
                if (hit.distance > 1.35f)
                {
                    moveRight = true;
                }
                else
                {
                    moveRight = false;
                    canAttack = true;
                }
            }

            if (moveleft)
            {
                transform.Translate(new Vector2(-1f, 0f) * Time.deltaTime * playerSpeed);
            }
            else if (inputManager.CurrentInput.y != 0.0 && !inputManager.isjumping)
            {
                rb.velocity = new Vector2(0.0f, jumpHeight);
            }
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
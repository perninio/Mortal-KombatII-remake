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
    public float jumpHeight = 10f;
    public Rigidbody2D rb;
    public int health = 100;
    public Animator fire;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animationController = GetComponent<Animator>();
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
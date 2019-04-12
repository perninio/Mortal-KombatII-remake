using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour

{

    InputManager inputManager;
    Animator animationController;


   [SerializeField] float playerSpeed = 10f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animationController = GetComponent<Animator>();
    }

    void Update()
    {
        animationController.SetBool("ismoving",inputManager.ismoving);
        animationController.SetBool("iskneeling",inputManager.iskneeling);
        animationController.SetBool("isjumping", inputManager.isjumping);
        animationController.SetBool("isblocking",inputManager.isblocking);
        animationController.SetBool("iskicking",inputManager.iskicking);
        animationController.SetBool("ispunching", inputManager.ispunching);
        animationController.SetFloat("punch1", inputManager.randNumber);
        whenactionstopmoving();
        transform.Translate(inputManager.CurrentInput * Time.deltaTime * playerSpeed);

    }

    private void whenactionstopmoving()
    {
        if (inputManager.iskneeling == true || inputManager.iskicking == true || inputManager.ispunching == true)
            playerSpeed = 0;
        else
            playerSpeed = 10;
    }
}
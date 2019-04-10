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

        transform.Translate(inputManager.CurrentInput * Time.deltaTime * playerSpeed);
    }
}
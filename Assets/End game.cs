using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    // Start is called before the first frame update
    Bot enemy;
    public Animator enemyanimator;
    
    void Start()
    {
       enemy= GameObject.Find("bot").GetComponent<Bot>();
        enemyanimator=GameObject.Find("bot").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.health>=0)
        {
            enemyanimator.SetBool("stunt", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    // Start is called before the first frame update
    Bot enemy;
    public Animator enemyanimator;//bot animator
    public Animator scorpionanimator;//narazie zrobię finishera dla scorpiona. Zautomatyzowanie można ogarnąć później

    public bool finish_him=true;
    public AudioSource finishhim;
    public static bool enableFinisher;

    
    void Start()
    {
       enemy= GameObject.Find("bot").GetComponent<Bot>();
        enemyanimator=GameObject.Find("bot").GetComponent<Animator>();
        finishhim = GetComponent<AudioSource>();
        scorpionanimator = GameObject.Find("scorpion").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.health<=0)
        {
            enemyanimator.SetBool("stunt", true);

            if(finish_him)
            {
               finish_him=false;
                finishhim.Play(0);
                enableFinisher = true;
                
            }
            scorpionanimator.SetInteger("FinishEnable", 2);
        }
    }
}

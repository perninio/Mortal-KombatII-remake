using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getoverhere : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool ifhit = false;
    public Animator scorpionAnimator;

    void Start()
    {
        scorpionAnimator=GameObject.Find("scorpion").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ifhit)
            scorpionAnimator.SetBool("ifhit",true);

    }
}

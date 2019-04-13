using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour
{
    public GameObject scorpion;
    public GameObject subzero;
    void Start()
    {
        if (ChosePlayer.turnOnScorpion)
        {
            ChosePlayer.turnOnScorpion = false;
            scorpion.SetActive(true);
        }
        if (ChosePlayer.turnOnSub0)
        {
            subzero.SetActive(true);
            ChosePlayer.turnOnSub0 = false;
        }
    }


}

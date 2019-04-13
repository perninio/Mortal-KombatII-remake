using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChosePlayer : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool turnOnScorpion = false;
   public static bool turnOnSub0 = false;
    public GameObject obj;
    public void heroClicked()
    {
        if(obj.name=="ButtonScorpion")
        {
            turnOnScorpion = true;
        }
        else if(obj.name == "ButtonSub0")
        {
            turnOnSub0 = true;
        }
        MainMenu.canvas = true;
        SceneManager.LoadScene("game");
    }
}

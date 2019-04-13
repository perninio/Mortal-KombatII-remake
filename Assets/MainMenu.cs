using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static bool canvas = false;
    public static bool roundonefight=false;

    public void PlayGame()
    {
        SceneManager.LoadScene("ChooseCharacter");        
        roundonefight = true;
    }
   public void QuitGame()
    {
        Application.Quit();
    }
    
}

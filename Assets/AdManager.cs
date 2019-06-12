using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{

    public void ShowAD() {

        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

// Animation Event example
// Small example that can be called on each specified frame.
// The code is executed once per animation loop.

using UnityEngine;
using System.Collections;

public class testscript : MonoBehaviour
{
    static public bool enemydissappear=false;
    public void PrintEvent()
    {
        GameObject.Find("bot").SetActive(false);
        Debug.Log("PrintEvent");
    }
}
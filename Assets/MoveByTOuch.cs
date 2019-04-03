using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTOuch : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {
            Touch touch0 = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch0.position);
            touchPosition.z = 0f;
            transform.position = touch0.position;


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFireballTest : MonoBehaviour
{
    void Start() {
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = GetComponent<Transform>();
        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

    }
}

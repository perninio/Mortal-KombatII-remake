using System.Collections;
using UnityEngine;

public class Combo : MonoBehaviour
{

    public GameObject[] special_hits;
    public string[] moves;
    private ArrayList currentCombo;

    // Timeout
    public float timeout = 1f;
    public float nextTimeout;
    private float nextmove = 0f;
    private float nextmoveRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        currentCombo = new ArrayList();
    }

    void Update() {
        timeoutMove();
    }

    private void timeoutMove() {
        if (Time.time > nextTimeout && currentCombo.Count > 0) {
            nextTimeout = Time.time + timeout;
            currentCombo.RemoveAt(0);
        }
    }

    public void addMove(string move) {
        // Check if move was not performed too quick
        if (Time.time > nextmove) {
            nextmove = Time.time + nextmoveRate;
        }
        else {
            return;
        }

        nextTimeout = Time.time + timeout;

        if (currentCombo.Count < 3)
        {
            currentCombo.Add(move);
        }
        else {
            currentCombo.RemoveAt(0);
            currentCombo.Add(move);
        }

        string fulllist = "";
        foreach (var item in currentCombo)
        {
            fulllist += item;
        }
        Debug.Log(fulllist);
    }


    public bool checkCombo() {
        string combostr = "";
        for (int i = 0; i < 3; i++) {
            combostr += currentCombo[i];
        }

        foreach (var item in moves)
        {
            if (!combostr.Equals(item))
                return false;
            else {
                Transform transform = GetComponent<Transform>();
                float x = transform.position.x;
                float y = transform.position.y;
                float z = transform.position.z;
                Instantiate(special_hits[0], new Vector3(x, y-0.3f, z), Quaternion.identity);
            }
        }
        return true;
    }

}

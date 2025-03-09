using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UI : MonoBehaviour {
    public GameObject Mind;
    public TMP_Text sample;
    
    void Start() {
        Mind = GameObject.Find("Mind");
    }
    
    void Update () {
        if (Mind.transform.position.x == 3.3f)
        {
            sample.text = "BLUE WINS";
            sample.color = Color.blue;
        }
        if (Mind.transform.position.x == -3.3f)
        {
            sample.text = "RED WINS";
            sample.color = Color.red;
        }
    }
}

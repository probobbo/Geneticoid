using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndingScore : MonoBehaviour {
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Your score:\n" + gamemanager.score;
    }
}

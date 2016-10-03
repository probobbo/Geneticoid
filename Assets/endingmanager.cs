using UnityEngine;
using System.Collections;

public class endingmanager : MonoBehaviour {

    public GameObject win, lose;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("GameManager").GetComponent<gamemanager>().won)
        {
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

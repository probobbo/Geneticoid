using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int health;
    GameObject hptext;

	// Use this for initialization
	void Start () {
        hptext = GameObject.Find("HPText");
        health = 10;
        hptext.GetComponent<Text>().text = health.ToString()+"x";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void PlayerDamage(int dmgs)
    {
        health -= dmgs;
        hptext.GetComponent<Text>().text = health.ToString()+"x";
        if (health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<gamemanager>().won = false;
            GameObject.Find("GameManager").GetComponent<gamemanager>().LoadScene(2);
        }
    }
}

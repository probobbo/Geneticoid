using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
        health = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void PlayerDamage(int dmgs)
    {
        health -= dmgs;
        if (health <= 0)
        {
            SceneManager.LoadScene("mainscene");
        }
    }
}

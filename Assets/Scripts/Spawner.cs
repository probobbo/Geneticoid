using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject swarm;

	// Use this for initialization
	void Start () {
	    for(int i = 0;  i<15; i++)
        {
            Instantiate(enemy , new Vector3(Random.Range(-40,40), Random.Range(-40, 40),0), Quaternion.identity);
        }
        swarm.SendMessage("create_array");
	}

    void Update()
    {
    }
}

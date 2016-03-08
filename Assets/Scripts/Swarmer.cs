using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swarmer : MonoBehaviour {

    List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    void create_array()
    {
        enemies =new List<GameObject>( GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void remove_enemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}

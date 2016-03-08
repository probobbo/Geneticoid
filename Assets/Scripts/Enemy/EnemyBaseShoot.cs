using UnityEngine;
using System.Collections;

public class EnemyBaseShoot : MonoBehaviour {
    float timer;
    public float threshold;
    public GameObject laserBeam;
    public GameObject laserSpawnPoint; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime; 
        if(timer >= threshold)
        {
            Shoot();
        }
	}


    void Shoot()
    {
        timer = 0f;
        Instantiate(laserBeam, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation);
    }
}

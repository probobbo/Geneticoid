using UnityEngine;
using System.Collections;

public class EnemyBaseShoot : MonoBehaviour {

    bool playerinsight = false;
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
            if (playerinsight)
            {
                RaycastHit2D ray = Physics2D.Raycast(laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation * Vector2.up);
                if (ray.collider.tag == "Player")
                {
                    Shoot();
                }
                timer = 0; 
            }
        }
	}


    void Shoot()
    {
        timer = 0f;
        GameObject laser = (GameObject)Instantiate(laserBeam, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation);
        EnemyHurt enemy = transform.gameObject.GetComponent<EnemyHurt>();
        laser.SendMessage("SetIndex",enemy.index);
    }

    void SetThreshold(float threshold)
    {
        this.threshold = threshold;
    }

    void SetSight(bool value)
    {
        this.playerinsight = value;
    }
}

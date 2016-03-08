using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject laserBeam;
    public GameObject laserSpawnPoint;
    public float timer;
    public float shootFrequency; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && timer >= shootFrequency)
        
        if(Input.GetMouseButton(0) && timer >= shootFrequency) {
            Shoot();
        }

        
	}

    void Shoot()
    {
        timer = 0f;
        Instantiate(laserBeam, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation); 
    } 
}

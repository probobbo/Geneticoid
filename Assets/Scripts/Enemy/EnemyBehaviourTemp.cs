using UnityEngine;
using System.Collections;

public class EnemyBehaviourTemp : MonoBehaviour {

    public float speed = 4;
    InterfaceEnemyMovement moveEngine; 

	// Use this for initialization
	void Start () {
        moveEngine = new EnemyFollowMovement(speed); 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            moveEngine = new EnemyFollowMovement(speed);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            moveEngine = new EnemyRandomMovement(speed);
        }
        //Movements of the enemy
        ArrayList movement = moveEngine.Move(transform.position,transform.rotation);
        transform.position = (Vector3)movement[0];
        transform.rotation = (Quaternion)movement[1];
    }
}

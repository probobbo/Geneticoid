using UnityEngine;
using System.Collections;

public class EnemyBehaviourTemp : MonoBehaviour {

    public float speed = 4;
    InterfaceEnemyMovement moveEngine; 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Movements of the enemy
        ArrayList movement = moveEngine.Move(transform.position,transform.rotation);
        transform.position = (Vector3)movement[0];
        transform.rotation = (Quaternion)movement[1];
    }

    void SetMoveEngine(InterfaceEnemyMovement moveEngine)
    {
        this.moveEngine = moveEngine; 
    }
}

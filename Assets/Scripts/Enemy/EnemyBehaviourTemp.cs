using UnityEngine;
using System.Collections;

public class EnemyBehaviourTemp : MonoBehaviour {

    public float speed = 4;
    InterfaceEnemyMovement moveEngine; 

	// Use this for initialization
	void Start () {
        moveEngine = new EnemyCircleMovement(speed); 
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
        moveEngine.Move(transform);
    }
}

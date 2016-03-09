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
        moveEngine.Move(transform);
    }

    void SetMoveEngine(InterfaceEnemyMovement moveEngine)
    {
        this.moveEngine = moveEngine; 
    }
}

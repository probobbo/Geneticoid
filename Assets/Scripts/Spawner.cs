using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject swarm;
    public int numberOfEnemies = 15; 

	// Use this for initialization
	void Start () {
	    for(int i = 0;  i<numberOfEnemies; i++)
        {
            GameObject temp = (GameObject)Instantiate(enemy , new Vector3(Random.Range(-40,40), Random.Range(-40, 40),0), Quaternion.identity);
            if (i > 2*numberOfEnemies/3)
                temp.SendMessage("SetMoveEngine", new EnemyRandomMovement(temp.GetComponent<EnemyBehaviourTemp>().speed));
            else if(i>numberOfEnemies/3)
                temp.SendMessage("SetMoveEngine", new EnemyFollowMovement(temp.GetComponent<EnemyBehaviourTemp>().speed));
            else
                temp.SendMessage("SetMoveEngine", new EnemyCircleMovement(temp.GetComponent<EnemyBehaviourTemp>().speed));
        }
        swarm.SendMessage("create_array");
	}

    void Update()
    {
    }
}

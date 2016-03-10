using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject swarm;
    public int numberOfEnemies = 15;
    int remaining;
    public int[] enemyhit;
    public float[] enemylifetime;

	// Use this for initialization
	void Start () {
        remaining = numberOfEnemies;
        GameObject.Find("EnemyText").GetComponent<Text>().text = "x"+remaining.ToString();
        enemyhit = new int[numberOfEnemies];
        enemylifetime = new float[numberOfEnemies];
        for (int i = 0;  i<numberOfEnemies; i++)
        {
            enemyhit[i] = 0;
            GameObject temp = (GameObject)Instantiate(enemy , new Vector3(Random.Range(-40,40), Random.Range(-40, 40),0), Quaternion.identity);
            temp.SendMessage("SetIndex", i);
            temp.SendMessage("SetSightrange", 12);
            temp.SendMessage("SetFirerange", 8);
            if (i > 2 * numberOfEnemies / 3)
            {
                temp.SendMessage("SetSpeed", 3);
                temp.SendMessage("SetThreshold", 0.5);
                temp.SendMessage("SetMoveEngine", new EnemyRandomMovement(temp.GetComponent<EnemyBehaviourTemp>().speed));
            }
                
            else if (i > numberOfEnemies / 3)
            {
                temp.SendMessage("SetSpeed", 2);
                temp.SendMessage("SetThreshold", 0.8);
                temp.SendMessage("SetMoveEngine", new EnemyFollowMovement(temp.GetComponent<EnemyBehaviourTemp>().speed));
            }

            else
            {
                temp.SendMessage("SetSpeed", 2.5);
                temp.SendMessage("SetThreshold", 1);
                temp.SendMessage("SetMoveEngine", new EnemyCircleMovement(temp.GetComponent<EnemyBehaviourTemp>().speed));
            }  
        }
        swarm.SendMessage("create_array");
	}

    void Update()
    {
    }

    void PlayerGotHit(int i)
    {
        this.enemyhit[i] += 1;
    }
    void StoreLifeTime(object[] obj)
    {
        remaining -= 1;
        GameObject.Find("EnemyText").GetComponent<Text>().text = "x" + remaining.ToString();
        enemylifetime[(int)obj[0]] = (float)obj[1];
    }
}

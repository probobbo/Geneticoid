using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject swarm;
    public int numberOfEnemies = 15;
    int remaining;
    public GeneticFeatures[] enemyData;
    FileWriter fw; 

    // Use this for initialization
    void Start()
    {
        fw = new FileWriter();
        enemyData = new GeneticFeatures[numberOfEnemies];
        remaining = numberOfEnemies;
        GameObject.Find("EnemyText").GetComponent<Text>().text = "x" + remaining.ToString();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float speed = 0;
            float sightRange = 0;
            float fireRange = 0;
            float threshold = 0;
            InterfaceEnemyMovement moveEngine = null; 

            enemyData[i] = new GeneticFeatures(); 
            //ci dovrebbe essere una lettura da file per spawnare i nemici 
            GameObject temp = (GameObject)Instantiate(enemy, new Vector3(Random.Range(-40, 40), Random.Range(-40, 40), 0), Quaternion.identity);

            temp.SendMessage("SetIndex", i);
            enemyData[i].index = i;

            sightRange = 12; 
            temp.SendMessage("SetSightrange", sightRange);
            enemyData[i].sightrange = sightRange;

            fireRange = 8; 
            temp.SendMessage("SetFirerange", fireRange);
            enemyData[i].firerange = fireRange;

            if (i > 2 * numberOfEnemies / 3)
            {
                speed = 3;
                threshold = 0.5f;
                moveEngine = new EnemyRandomMovement(speed); 
            }

            else if (i > numberOfEnemies / 3)
            {
                speed = 2;
                threshold = 0.8f;
                moveEngine = new EnemyFollowMovement(speed);
            }

            else
            {
                speed = 2.5f;
                threshold = 1;
                moveEngine= new EnemyCircleMovement(speed);
            }

            temp.SendMessage("SetSpeed", speed);
            enemyData[i].speed = speed; 
            temp.SendMessage("SetThreshold", threshold);
            enemyData[i].threshold = threshold; 
            temp.SendMessage("SetMoveEngine", moveEngine);
            enemyData[i].movement = moveEngine.ToString();
        }
        swarm.SendMessage("create_array");
    }

    void Update()
    {
    }

    void PlayerGotHit(int i)
    {
        enemyData[i].hits += 1; 
    }
    void StoreLifeTime(object[] obj)
    {
        remaining -= 1;
        GameObject.Find("EnemyText").GetComponent<Text>().text = "x" + remaining.ToString();
        enemyData[(int)obj[0]].lifetime =((float)obj[1]);
        if (remaining == 0)
        {
            fw.AppendString("\"Gen\":{");
            for (int i = 0; i < enemyData.Length; i++)
            {
                if(i==0)
                    fw.AppendString("\"" + i + "\":" + JsonUtility.ToJson(enemyData[i]));
                else
                    fw.AppendString(",\"" + i + "\":" + JsonUtility.ToJson(enemyData[i]));
            }
            fw.AppendString("}");
        }
    }
}

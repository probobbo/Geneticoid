using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    string[] movements = { "EnemyCircleMovement", "EnemyFollowMovement", "EnemyRandomMovement" };
    public GameObject enemy;
    public GameObject swarm;
    public int numberOfEnemies = 15;
    public int remaining;
    public GeneticFeatures[] enemyData;
    FileWriter fw,fw2; 

    // Use this for initialization
    void Start()
    {
        // just a debug section 
        //GeneticFeatures g = GeneticFeatures.CreateFromJSON("\"Gen\":{\"0\":{ \"index\":0,\"lifetime\":5.774858474731445,\"hits\":0,\"speed\":2.5,\"movement\":\"EnemyCircleMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":1.0},\"1\":{ \"index\":1,\"lifetime\":35.86322784423828,\"hits\":1,\"speed\":2.5,\"movement\":\"EnemyCircleMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":1.0},\"2\":{ \"index\":2,\"lifetime\":20.174379348754884,\"hits\":0,\"speed\":2.0,\"movement\":\"EnemyFollowMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":0.800000011920929}}");
        //Debug.Log(g.movement);
        // Debug.Log(g.index);

        GeneticFeatures[] NewGen =  GameObject.Find("GameManager").GetComponent<gamemanager>().LoadLastGen();
        // end of debug section
        NewGen = Genetication(NewGen);
        fw = new FileWriter("Generation");
        fw2 = new FileWriter("LastGen");
        enemyData = new GeneticFeatures[numberOfEnemies];
        remaining = numberOfEnemies;

        UpdateText();

        for (int i = 0; i < numberOfEnemies; i++)
        {
            float speed = 0;
            float sightRange = 0;
            float fireRange = 0;
            float threshold = 0;
            InterfaceEnemyMovement moveEngine = null; 

            enemyData[i] = new GeneticFeatures(); 
            //ci dovrebbe essere una lettura da file per spawnare i nemici 
            GameObject temp = (GameObject)Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-40, 40), UnityEngine.Random.Range(-40, 40), 0), Quaternion.identity);

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

    GeneticFeatures[] Genetication(GeneticFeatures[] last)
    {
        GeneticFeatures[] temp = (GeneticFeatures[])last.Clone();
        temp = Selection(temp);
        temp = Recombination(temp);
        temp = Mutation(temp);
        return temp;
    }

    GeneticFeatures[] Selection (GeneticFeatures[] last)
    {
        Array.Sort(last, delegate (GeneticFeatures user1, GeneticFeatures user2) {return user1.Fitness().CompareTo(user2.Fitness());});
        Array.Reverse(last);
        GeneticFeatures[] temp = new GeneticFeatures[5];
        Array.Copy(last, temp, 5);
        return temp;
    }

    GeneticFeatures[] Recombination (GeneticFeatures[] last)
    {
        int i = 0;
        List<GeneticFeatures> temp = new List<GeneticFeatures>();
        foreach(GeneticFeatures g1 in last)
        {
            foreach(GeneticFeatures g2 in last)
            {
                if(g1.index != g2.index)
                {
                    temp.Add(crossing(g1, g2,i));
                    i++;
                }
            }
        }
        return temp.ToArray();
    }

    GeneticFeatures[] Mutation(GeneticFeatures[] last)
    {
        foreach(GeneticFeatures g in last)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= 0.1f)
            {
                int val = UnityEngine.Random.Range(0, 4);
                switch (val)
                {
                    case 0:
                        g.speed = UnityEngine.Random.Range(2, 10);
                        break;
                    case 1: 
                        g.movement = movements[UnityEngine.Random.Range(0, 2)];
                        break;
                    case 2:
                        g.sightrange = UnityEngine.Random.Range(5, 20);
                        break;
                    case 3:
                        g.firerange = UnityEngine.Random.Range(5, 16);
                        break;
                    case 4:
                        g.threshold = UnityEngine.Random.Range(0.2f, 1.5f);
                        break;
                }
            }
        }
        return last;
    }

    GeneticFeatures crossing (GeneticFeatures g1, GeneticFeatures g2,int i)
    {
        GeneticFeatures ret = new GeneticFeatures();
        ret.index = i;
        ret.speed = g1.speed;
        ret.movement = g1.movement;
        ret.sightrange = g2.sightrange;
        ret.firerange = g2.firerange;
        ret.threshold = g2.threshold;
        return ret;
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
        UpdateText();
        enemyData[(int)obj[0]].lifetime =((float)obj[1]);
        if (remaining == 0)
        {
            WriteToFile();
            GameObject.Find("GameManager").GetComponent<gamemanager>().won = true;
            GameObject.Find("GameManager").GetComponent<gamemanager>().LoadScene(2);
        }
    }


    void WriteToFile()
    {
        fw.WriteString("\"Gen\":{");
        for (int i = 0; i < enemyData.Length; i++)
        {
            if (i == 0)
            {
                fw.WriteString("\"" + i + "\":" + JsonUtility.ToJson(enemyData[i]));
                fw2.WriteString(JsonUtility.ToJson(enemyData[i]),false);
            }
            else {
                fw.WriteString(",\"" + i + "\":" + JsonUtility.ToJson(enemyData[i]));
                fw2.WriteString(JsonUtility.ToJson(enemyData[i]));
            }
            
        }
        fw.WriteString("}");
       
    }

    void UpdateText()
    {
        GameObject.Find("EnemyText").GetComponent<Text>().text = "x" + remaining.ToString();
    }

    void GenerateEnemiesAndFillEnemyData()
    {

    }
}

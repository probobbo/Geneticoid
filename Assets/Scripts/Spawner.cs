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
    FileWriter fw, fw2;
    GeneticFeatures[] NewGen;

    // Use this for initialization
    void Start()
    {
        // just a debug section 
        //GeneticFeatures g = GeneticFeatures.CreateFromJSON("\"Gen\":{\"0\":{ \"index\":0,\"lifetime\":5.774858474731445,\"hits\":0,\"speed\":2.5,\"movement\":\"EnemyCircleMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":1.0},\"1\":{ \"index\":1,\"lifetime\":35.86322784423828,\"hits\":1,\"speed\":2.5,\"movement\":\"EnemyCircleMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":1.0},\"2\":{ \"index\":2,\"lifetime\":20.174379348754884,\"hits\":0,\"speed\":2.0,\"movement\":\"EnemyFollowMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":0.800000011920929}}");
        //Debug.Log(g.movement);
        // Debug.Log(g.index);
        if (GameObject.Find("GameManager").GetComponent<gamemanager>().first)
        {
            GameObject.Find("GameManager").GetComponent<gamemanager>().first = false;
            NewGen = FirstGen(GameObject.Find("GameManager").GetComponent<gamemanager>().number);

        }
        else if (!GameObject.Find("GameManager").GetComponent<gamemanager>().won && GameObject.Find("GameManager").GetComponent<gamemanager>().lastgen.Length!=0)
        {
            NewGen = GameObject.Find("GameManager").GetComponent<gamemanager>().lastgen;
        }
        else
        {
            NewGen = GameObject.Find("GameManager").GetComponent<gamemanager>().LoadLastGen();
            NewGen = Genetication(NewGen);
        }
        fw = new FileWriter("Generation");
        fw2 = new FileWriter("LastGen");
        enemyData = new GeneticFeatures[NewGen.Length];
        //remaining = numberOfEnemies;
        remaining = NewGen.Length;

        UpdateText();

        for (int i = 0; i < NewGen.Length; i++)
        {
            float speed = 0;
            float sightRange = 0;
            float fireRange = 0;
            float threshold = 0;
            int life = 0;
            InterfaceEnemyMovement moveEngine = null;

            enemyData[i] = new GeneticFeatures();
            float x = UnityEngine.Random.Range(-38, 38);
            while(x>-10 && x < 10)
            {
                x = UnityEngine.Random.Range(-38, 38);
            }
            float y = UnityEngine.Random.Range(-38, 38);
            while (y > -10 && y < 10)
            {
                y = UnityEngine.Random.Range(-38, 38);
            }
            //ci dovrebbe essere una lettura da file per spawnare i nemici 
            GameObject temp = (GameObject)Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);

            temp.SendMessage("SetIndex", NewGen[i].index);
            enemyData[i].index = NewGen[i].index;

            sightRange = NewGen[i].sightrange;
            temp.SendMessage("SetSightrange", sightRange);
            enemyData[i].sightrange = sightRange;

            fireRange = NewGen[i].firerange;
            temp.SendMessage("SetFirerange", fireRange);
            enemyData[i].firerange = fireRange;
            speed = NewGen[i].speed;
            threshold = NewGen[i].threshold;
            life = NewGen[i].life;
            switch (NewGen[i].movement)
            {
                case "EnemyCircleMovement":
                    moveEngine = new EnemyCircleMovement(speed);
                    break;
                case "EnemyFollowMovement":
                    moveEngine = new EnemyFollowMovement(speed);
                    break;
                case "EnemyRandomMovement":
                    moveEngine = new EnemyRandomMovement(speed);
                    break;
            }
            temp.SendMessage("setlife", life);
            enemyData[i].life = life;
            temp.SendMessage("SetSpeed", speed);
            enemyData[i].speed = speed; 
            temp.SendMessage("SetThreshold", threshold);
            enemyData[i].threshold = threshold; 
            temp.SendMessage("SetMoveEngine", moveEngine);
            enemyData[i].movement = moveEngine.ToString();
            GameObject.Find("GameManager").GetComponent<gamemanager>().lastgen = NewGen;
        }
        //swarm.SendMessage("create_array");
    }

    GeneticFeatures[] FirstGen(int n_elem)
    {
        List<GeneticFeatures> temp = new List<GeneticFeatures>();
        for (int i = 0; i < n_elem; i++)
        {
            temp.Add(RandomGeneticFeature(i));
        }
        return temp.ToArray();
    }

    GeneticFeatures RandomGeneticFeature(int index)
    {
        GeneticFeatures g = new GeneticFeatures();
        g.index = index;
        g.speed = UnityEngine.Random.Range(2, 10);
        g.movement = movements[UnityEngine.Random.Range(0, 2)];
        g.sightrange = UnityEngine.Random.Range(5, 20);
        g.firerange = UnityEngine.Random.Range(5, 16);
        g.threshold = UnityEngine.Random.Range(0.2f, 1.5f);
        g.life = UnityEngine.Random.Range(1, 4);
        return g;
    }

    GeneticFeatures[] Genetication(GeneticFeatures[] last)
    {
        GeneticFeatures[] temp = (GeneticFeatures[])last.Clone();
        temp = Selection(temp);
        temp = Recombination(temp);
        temp = Mutation(temp);
        return temp;
    }

    GeneticFeatures[] Selection(GeneticFeatures[] last)
    {
        Array.Sort(last, delegate (GeneticFeatures user1, GeneticFeatures user2) { return user1.Fitness().CompareTo(user2.Fitness()); });
        Array.Reverse(last);
        GeneticFeatures[] temp = new GeneticFeatures[5];
        Array.Copy(last, temp, 5);
        return temp;
    }

    GeneticFeatures[] Recombination(GeneticFeatures[] last)
    {
        int i = 0;
        List<GeneticFeatures> temp = new List<GeneticFeatures>();
        foreach (GeneticFeatures g1 in last)
        {
            foreach (GeneticFeatures g2 in last)
            {
                if (g1.index != g2.index)
                {
                    temp.Add(crossing(g1, g2, i));
                    i++;
                }
            }
        }
        return temp.ToArray();
    }

    GeneticFeatures[] Mutation(GeneticFeatures[] last)
    {
        foreach (GeneticFeatures g in last)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= 0.1f)
            {
                int val = UnityEngine.Random.Range(0, 5);
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
                    case 5:
                        g.life = UnityEngine.Random.Range(1,4);
                        break;
                }
            }
        }
        return last;
    }

    GeneticFeatures crossing(GeneticFeatures g1, GeneticFeatures g2, int i)
    {
        GeneticFeatures ret = new GeneticFeatures();
        ret.index = i;
        ret.speed = g1.speed;
        ret.movement = g1.movement;
        ret.life = g1.life;
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
        enemyData[(int)obj[0]].lifetime = ((float)obj[1]);
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
                fw2.WriteString(JsonUtility.ToJson(enemyData[i]), false);
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

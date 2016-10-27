using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyBehaviourTemp : MonoBehaviour {


    public float speed = 4;
    public float sightrange = 12;
    public float firerange = 8;
    public string movement;
    public float chasebyadvicetime = 5f;
    public float advicerange = 15f;
    bool timer = false;
    InterfaceEnemyMovement baseEngine;
    public InterfaceEnemyMovement moveEngine;
    Transform player;

    IEnumerator StartChasing()
    {
        timer = true;
        yield return new WaitForSeconds(chasebyadvicetime);
        timer = false;
    }

    public void Advice(Transform position)
    {
        if (!timer && Vector2.Distance(transform.position, position.position) < advicerange)
        {
            print("Adviced!");
            StartCoroutine(StartChasing());
        }
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseEngine = new EnemyRandomMovement(speed);
        
    }

	// Update is called once per frame
	void Update () {
        //Movements of the enemy
        if (timer || PlayerInSight())
            moveEngine.Move(transform);
        else
            baseEngine.Move(transform);
        if (PlayerInRange())
            SendMessage("SetSight",true);
        else
            SendMessage("SetSight",false);
    }

    void SetMoveEngine(InterfaceEnemyMovement moveEngine)
    {
        this.moveEngine = moveEngine;
        movement = (moveEngine.ToString()).Substring(5);
    }

    void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    
    bool PlayerInSight()
    {
        bool ret = Vector3.Distance(transform.position, player.position) <= sightrange;
        if (ret)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject g in enemies)
            {
                g.SendMessage("Advice", transform);
            }
        }
        return ret;
    }
    
    bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= firerange;
    }
    
    void SetFirerange(float range)
    {
        this.firerange = range;
    }

    void SetSightrange(float range)
    {
        this.sightrange = range;
    }

    void SetAdviceRange(float ar)
    {
        this.advicerange = ar;
    }
}

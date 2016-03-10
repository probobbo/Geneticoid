using UnityEngine;
using System.Collections;

public class EnemyBehaviourTemp : MonoBehaviour {

    public float speed = 4;
    public float sightrange = 12;
    public float firerange = 8;
    public string movement; 
    InterfaceEnemyMovement baseEngine;
    public InterfaceEnemyMovement moveEngine;
    Transform player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseEngine = new EnemyRandomMovement(speed);
    }

	// Update is called once per frame
	void Update () {
        //Movements of the enemy
        if (PlayerInSight())
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
        return Vector3.Distance(transform.position, player.position) <= sightrange;
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
}

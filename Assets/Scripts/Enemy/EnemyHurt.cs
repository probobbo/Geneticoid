using UnityEngine;
using System.Collections;

public class EnemyHurt : MonoBehaviour
{
    GameObject enemytext;
    public int index;
    public GameObject explosion;
    public GameObject swarm;
    GameObject spawner;
    public int life;
    public float lifetime = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (life == 0)
        {
            spawner = GameObject.FindGameObjectWithTag("Spawner");
            swarm = GameObject.FindWithTag("Swarmer");
            swarm.SendMessage("remove_enemy", transform.gameObject);
            object[] param = new object[2] { index, lifetime };
            spawner.SendMessage("StoreLifeTime",param);
            Destruction();
        }
    }

    void Damage(int damage)
    {
        life -= damage; 
    }

    void Destruction()
    {
        //show explosion
        Instantiate(explosion, transform.position, transform.rotation); 
        Destroy(transform.gameObject);
    }

    void SetIndex(int i)
    {
        this.index = i;
    }
}

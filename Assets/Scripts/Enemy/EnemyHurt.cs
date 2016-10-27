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

    public void setlife(int l)
    {
        life = l;
    }
    // Use this for initialization
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (life == 0)
        {
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
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        swarm = GameObject.FindWithTag("Swarmer");
        swarm.SendMessage("remove_enemy", transform.gameObject);
        object[] param = new object[2] { index, lifetime };
        spawner.SendMessage("StoreLifeTime", param);
        Instantiate(explosion, transform.position, transform.rotation); 
        Destroy(transform.gameObject);
    }

    void SetIndex(int i)
    {
        this.index = i;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessageUpwards("PlayerDamage", 1);
            spawner.SendMessage("PlayerGotHit", index);
            Destruction();
        }
    }
}

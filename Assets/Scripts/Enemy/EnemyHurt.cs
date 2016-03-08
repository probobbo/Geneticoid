using UnityEngine;
using System.Collections;

public class EnemyHurt : MonoBehaviour
{
    public GameObject explosion;
    public GameObject swarm;
    public int life;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            swarm = GameObject.FindWithTag("Swarmer");
            swarm.SendMessage("remove_enemy", transform.gameObject);
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
}

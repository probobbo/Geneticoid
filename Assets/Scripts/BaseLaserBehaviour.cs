using UnityEngine;
using System.Collections;

public class BaseLaserBehaviour : MonoBehaviour
{
    public float timeToLive;
    float timer;
    public float speed = 15;
    public int dmgs = 1; 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLive)
            Destroy(transform.gameObject);
        Quaternion rot = transform.rotation;
        Vector3 velocity = new Vector3(0, Time.deltaTime * speed);
        transform.position += rot * velocity;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.SendMessageUpwards("Damage", dmgs);
            Destroy(transform.gameObject);
        }
    }
}

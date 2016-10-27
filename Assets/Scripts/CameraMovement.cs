using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    GameObject player;
    public int speed = 100;
    float vertical;
    float horizontal; 
    // Use this for initialization
    void Start()
    {
        vertical = Camera.main.orthographicSize;
        horizontal = vertical * Screen.width / Screen.height; 
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
   
            Vector3 pos = player.transform.position;
            //Vector3 velocity = new Vector3(pos.x - transform.position.x, pos.y - transform.position.y, 0);
            //transform.position = transform.position + velocity.normalized * Time.deltaTime * speed;

            transform.position = new Vector3(Mathf.Clamp(pos.x, -40.0f + horizontal, 40.0f - horizontal), Mathf.Clamp(pos.y, -40.0f + vertical, 40.0f - vertical), transform.position.z);
        }
    }
}

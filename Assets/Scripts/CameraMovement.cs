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
            
            //transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}

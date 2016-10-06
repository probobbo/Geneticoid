using UnityEngine;
using System.Collections;

public class ArrowManager
     : MonoBehaviour
{

    public GameObject arrow;
    public GameObject ball;


    // Use this for initialization
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Enemy");
     
    }

    // Update is called once per frame
    void Update()
    {
        if (ball != null)
        {
            Vector3 b_pos = transform.position - ball.transform.position;
            float height = GetComponent<Camera>().orthographicSize;
            float width = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
            //Debug.Log("Ball pos: " + b_pos + " heig: " + height + " width: " + width);
            if (b_pos.y > height || b_pos.y < -height || b_pos.x > width || b_pos.x < -width)
            {
                arrow.GetComponent<SpriteRenderer>().enabled = true;
                arrow.transform.position = ball.transform.position;
                arrow.transform.localPosition = new Vector3(Mathf.Clamp(arrow.transform.localPosition.x, -width, width), Mathf.Clamp(arrow.transform.localPosition.y, -height, height), arrow.transform.localPosition.z);
                Vector3 dir = ball.transform.position - arrow.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                arrow.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
            ball = GameObject.FindGameObjectWithTag("Enemy");
    }
}
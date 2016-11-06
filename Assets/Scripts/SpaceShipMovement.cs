using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpaceShipMovement : MonoBehaviour {
    public float speed = 5;
    public GameObject panel;
    public bool pause = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        //rotation
        if (!pause)
        {
            Vector3 mousePos = Input.mousePosition;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            //transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
            
        }
	}

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(Input.GetAxis("Horizontal") * speed,
            Input.GetAxis("Vertical") * speed, 0);
    }

    public void Pause()
    {
        if (!pause)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            pause = true;
        }
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1;
            pause = false;
        }
    }

    public void BackToMenu()
    {
        //Pause();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

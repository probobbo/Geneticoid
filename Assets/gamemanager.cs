using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour {

    public bool won = false;

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public GeneticFeatures[] LoadLastGen()
    {
        
        return new FileReader().ReadJSon();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour {

    public static float score = 0;
    public bool won = false;
    public bool first = true;
    public int number;
    public GeneticFeatures[] lastgen;

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetFirst(bool b)
    {
        first = b;
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public GeneticFeatures[] LoadLastGen()
    {
        
        return new FileReader().ReadJSon();
    }

    public void ResetScore()
    {
        score = 0;
    }
}

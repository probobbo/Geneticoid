﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour {

    public static float score = 0;
    public bool won = false;
    public bool first = true;
    public int number;
    public GeneticFeatures[] lastgen;
    public int wavwNumber = 0; 

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
        if (first)
        {
            PlayerPrefs.SetInt("Wave", 0);
            wavwNumber = PlayerPrefs.GetInt("Wave");
        }
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public GeneticFeatures[] LoadLastGen()
    {
        wavwNumber = PlayerPrefs.GetInt("Wave");
        return new FileReader().ReadJSon();
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void WonGen()
    {
        SceneManager.LoadScene(1);
        wavwNumber = PlayerPrefs.GetInt("Wave") + 1;
        PlayerPrefs.SetInt("Wave", wavwNumber);
    }


}

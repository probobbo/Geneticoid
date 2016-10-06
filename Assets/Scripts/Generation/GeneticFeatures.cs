using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GeneticFeatures {

    public int index;
    public float lifetime;
    public int hits;
    public float speed;
    public string movement;
    public float sightrange;
    public float firerange;
    public float threshold;
    public int life;

    public static GeneticFeatures CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GeneticFeatures>(jsonString);
    }

    public float Fitness()
    {
        return (lifetime * 0.1f) * (hits * 5f);
    }
}

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class FileReader
{

    public GeneticFeatures[] ReadJSon()
    {
        List<GeneticFeatures> lista = new List<GeneticFeatures>();
        StreamReader r = new StreamReader("Resources/LastGen.json");
        string l = r.ReadLine();
        while (l != null)
        {
            GeneticFeatures g = JsonUtility.FromJson<GeneticFeatures>(l);
            lista.Add(g);
            l = r.ReadLine();
        }
        return lista.ToArray();
    }
}

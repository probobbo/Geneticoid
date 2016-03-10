using UnityEngine;
using System.Collections;
using System.IO;

public class FileWriter {

    public string FileName = "Generation";
    private TextAsset asset; // Gets assigned through code. Reads the file.
    public StreamWriter writer; // This is the writer that writes to the file

    public void AppendString(string appendString)
    {
        asset = Resources.Load(FileName + ".json") as TextAsset;
        writer = new StreamWriter("Resources/" + FileName + ".json",true); // Does this work?
        writer.WriteLine(appendString);
        writer.Close();
    }
}

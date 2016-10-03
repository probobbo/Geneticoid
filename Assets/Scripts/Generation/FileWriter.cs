using UnityEngine;
using System.Collections;
using System.IO;

public class FileWriter {

    public string FileName;
    private TextAsset asset; // Gets assigned through code. Reads the file.
    public StreamWriter writer; // This is the writer that writes to the file

    public FileWriter(string name)
    {
        FileName = name;
    }

    public void WriteString(string appendString, bool append=true)
    {
        asset = Resources.Load(FileName + ".json") as TextAsset;
        writer = new StreamWriter("Resources/" + FileName + ".json",append); // Does this work?
        writer.WriteLine(appendString);
        writer.Close();
    }
}

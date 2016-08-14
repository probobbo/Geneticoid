using UnityEngine;
using System.Collections;

public class FileReader
{

    public ArrayList ReadJSon()
    {
        string a = "\"Gen\":{\"0\":{ \"index\":0,\"lifetime\":5.774858474731445,\"hits\":0,\"speed\":2.5,\"movement\":\"EnemyCircleMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":1.0},\"1\":{ \"index\":1,\"lifetime\":35.86322784423828,\"hits\":1,\"speed\":2.5,\"movement\":\"EnemyCircleMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":1.0},\"2\":{ \"index\":2,\"lifetime\":20.174379348754884,\"hits\":0,\"speed\":2.0,\"movement\":\"EnemyFollowMovement\",\"sightrange\":12.0,\"firerange\":8.0,\"threshold\":0.800000011920929}}";
        JSONObject pippo = new JSONObject(a);
        Debug.Log(pippo.ToDictionary());
        JSONObject son = pippo.GetField("0");
        if (son != null)
            Debug.Log(son.ToDictionary());
        // TODO: capire come interpretare i JSON con JSONObject!

        // float life = son.GetField("lifetime"); 
        return null;
    }
}

using UnityEngine;
using System.Collections;
using System;

public class EnemyFollowMovement : InterfaceEnemyMovement {

    float speed;
    Transform player; 

    public EnemyFollowMovement(float speed)
    {
        this.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    public ArrayList Move(Vector3 position, Quaternion rotation)
    {
        Vector3 dir = player.position - position;
        position += dir.normalized * speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

        ArrayList result = new ArrayList();
        result.Add(position);
        result.Add(rotation);
        return result;
    }
}

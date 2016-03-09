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

    public void Move(Transform transform)
    {
        Vector3 dir = player.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

    }
}

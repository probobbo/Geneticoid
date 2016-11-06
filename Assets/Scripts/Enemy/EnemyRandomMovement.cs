using UnityEngine;
using System.Collections;
using System;

public class EnemyRandomMovement : InterfaceEnemyMovement {

    float speed; 
    float timer;
    float threshold; 

    public EnemyRandomMovement(float speed)
    {
        this.speed = speed;
    }

    public void Move(Transform transform)
    {
        timer += Time.deltaTime;
        if (timer >= threshold)
        {
            timer = 0;
            threshold = UnityEngine.Random.value * 3;
            Quaternion rot = UnityEngine.Random.rotation;
            transform.rotation = new Quaternion(0, 0, rot.z, rot.w);
        }

        Vector3 velocity = new Vector3(0, Time.deltaTime * speed);
        
        transform.position += transform.rotation * velocity;
        ArrayList result = new ArrayList();
    }

}

using UnityEngine;
using System.Collections;
using System;

public class EnemyCircleMovement : InterfaceEnemyMovement
{
    public Vector3 axis = Vector3.forward;
    public Vector3 desiredPosition;
    public float radius = 1.0f;
    public float rotationSpeed = 40.0f;
    Transform player;
    float speed;

    public EnemyCircleMovement(float speed)
    {
        this.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Move(Transform transform)
    {
        transform.RotateAround(player.position, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - player.position).normalized * radius + player.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * speed);
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

}
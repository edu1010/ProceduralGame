using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Vector2 direction;

    float speed = 3.0f;

    Transform Player;

    float damage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        SetVelocityAndDir();
    }

    void SetVelocityAndDir()
    {
        direction = Player.position - transform.position;

        rigidbody2D.linearVelocity = direction * speed;
    }
}

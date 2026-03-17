using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletPlayer : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] float speed = 6.0f;


    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void SetVelocityAndDir(Vector2 direction)
    {
        rigidbody2D.linearVelocity = direction * speed;
    }
}

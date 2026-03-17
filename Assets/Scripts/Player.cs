using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepoint;

    Rigidbody2D rb;
    Camera mainCam;
    Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput.Normalize();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
            return;
        }

        transform.position += (Vector3)(moveInput * moveSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if (bulletPrefab == null)
        {
            return;
        }

        Transform origin = firepoint != null ? firepoint : transform;
        Vector2 dir = (Vector2)transform.right;
        if (mainCam != null)
        {
            Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
            dir = (Vector2)(mouseWorld - origin.position);
            if (dir.sqrMagnitude > 0.0001f)
            {
                dir.Normalize();
            }
            else
            {
                dir = transform.right;
            }
        }

        Quaternion rot = Quaternion.FromToRotation(Vector3.right, dir);
        GameObject bullet = Instantiate(bulletPrefab, origin.position, rot);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = dir * bulletSpeed;
        }
    }
}

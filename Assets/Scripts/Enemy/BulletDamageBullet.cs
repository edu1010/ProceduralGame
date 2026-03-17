using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageBullet : MonoBehaviour
{
        public float Damage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var reciever = collision.gameObject.GetComponent<IRecieveDamage>();
        if (reciever != null)
        {
            reciever.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
   
}

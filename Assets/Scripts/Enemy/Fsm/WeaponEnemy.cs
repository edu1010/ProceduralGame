using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform firepoint;

    private void OnEnable()
    {
        FlyEnemyAI.Fire += Fire;
    }

    private void OnDisable()
    {
        FlyEnemyAI.Fire -= Fire;
    }

    public void Fire()
    {
        Instantiate(BulletPrefab, firepoint.position, firepoint.rotation);
    }
}

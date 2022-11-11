using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public float baseDamage = 5;

    protected override void OnTriggerEnter(Collider other)
    {
        Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

        if (other.gameObject.CompareTag("Enemy"))
        {
            //other.GetComponent<BaseEnemyController>().TakeDamage(baseDamage);
            Destroy(gameObject);
        }
    }
}

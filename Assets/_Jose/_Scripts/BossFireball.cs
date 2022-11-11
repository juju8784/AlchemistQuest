using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : Bullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (hitEffect)
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

        if (other.gameObject.CompareTag("Player") && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            GameManager.instance.playerManager.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        BossGasAttack gasAttack = other.gameObject.GetComponent<BossGasAttack>();
        if (gasAttack)
        {
            gasAttack.Explode();
            gameObject.SetActive(false);
        }
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();

        transform.position = new Vector3(transform.position.x,
                                             GameManager.instance.player.transform.position.y,
                                             transform.position.z);
        Vector3 rotation = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(rotation);
    }
}

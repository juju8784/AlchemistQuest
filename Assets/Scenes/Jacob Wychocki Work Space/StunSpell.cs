using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSpell : BaseSpell
{
    BaseEnemyController enemy;
    bool active = false;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
                enemy = other.GetComponent<BaseEnemyController>();
                enemy.isStunned = true;
                GetComponent<Collider>().enabled = false;
                active = true;
                Execute(gameObject);
        }
    }

    protected override void Update()
    {
        if (active && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            if (Duration > 0)
                Duration -= Time.deltaTime;
            else
            {
                enemy.isStunned = false;
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
   
}

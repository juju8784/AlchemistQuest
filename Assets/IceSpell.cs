using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : BaseSpell
{


    protected override void Start()
    {
        Destroy(gameObject, Duration);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
            Execute(other.gameObject);
        }
    }

    protected override void Update()
    {
        if(GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : BaseSpell     
{
    bool active = false;
    GameObject Affected;
    BaseEnemyController enemy;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<BaseEnemyController>();
                enemy.IsFlammable = true;
                DealDamageToEnemy(enemy);
                GetComponent<Collider>().enabled = false;
                active = true;
                Execute(gameObject);
                Affected = other.gameObject;
        }
    }


    protected override void Update()
    {
        
        if (active)
        {
            Debug.Log(enemy.IsFlammable);
            if (Duration > 0)
                Duration -= Time.deltaTime;
            else
            {
                enemy.IsFlammable = false;
                Destroy(gameObject);
            }
        }



    }
}

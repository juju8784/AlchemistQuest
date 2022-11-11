using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisorientDebuff : BaseSpell
{
    // Start is called before the first frame update
    bool active = false;
    GameObject Affected;
    BaseEnemyController enemy;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<BaseEnemyController>();
            /* AddDisorient to enemy 
                 GetComponent<Collider>().enabled = false;
                 active = true;
                 Execute(gameObject);
                 Affected = other.gameObject;
             }
            */

        }
    }
    protected override void Update()
    {
        if (active)
        {
            if (Duration > 0)
                Duration -= Time.deltaTime;
            else
            {
                // Remove Disorient
                Destroy(gameObject);
            }
        }



    }
}

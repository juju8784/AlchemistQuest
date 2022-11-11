using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Spell : BaseSpell
{
    bool active = false;
    GameObject Affected;
    BaseEnemyController enemy;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<BaseEnemyController>();
            if (enemy.AddSlowStack() == false)
                Destroy(gameObject);
            else
            {
                GetComponent<Collider>().enabled = false;
                active = true;
                Execute(gameObject);
                Affected = other.gameObject;
            }
                
                
        }
    }

    
    protected override void Update()
    {
        if(active)
        {
            if (Duration > 0)
                Duration -= Time.deltaTime;
            else
            {
                enemy.RemoveSlowStack();
                Destroy(gameObject);
            }
        }
        

        
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    
}

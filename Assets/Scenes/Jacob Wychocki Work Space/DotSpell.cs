using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpell : BaseSpell
{
    [SerializeField] float Interval = 0;
    protected GameObject Attached;
    [SerializeField] protected GameObject hiteffect = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        Destroy(gameObject, 30);   
    }

    // Update is called once per frame

    float time;
    bool activated;
    protected override void Update()
    {
        if (activated)
        {
            transform.position = Attached.transform.position;
            if (Time.time - time > Interval)
            {
                Duration -= Interval;
                time = Time.time;
                Instantiate(hiteffect, transform.position, transform.rotation);
                
                if (enemy.CompareTag("Enemy"))
                        enemy.TakeDamage(Damage,Type);
                    

            }
            if (Duration < 0 || Attached.activeSelf == false)
            {
                Destroy(gameObject);
            }
        }

    }
    BaseEnemyController enemy;
    protected override void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider>().enabled = false;
            enemy = other.GetComponent<BaseEnemyController>();
            Attached = other.gameObject;
            activated = true;
            Execute(gameObject);
            
        }
    }
    
}

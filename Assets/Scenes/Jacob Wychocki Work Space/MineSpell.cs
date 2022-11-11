using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpell : BaseSpell
{
    public float size = 5;
    protected override void Start()
    {
        Destroy(gameObject, Duration);

    }
    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            Detonate();
    }

    protected override void Update()
    {
        if (Duration > 0)
            Duration -= Time.deltaTime;
        else
            Detonate();
    }

    protected void Detonate()
    {
        
        Collider[] Affected = Physics.OverlapSphere(transform.position, size);
        {
            foreach (var item in Affected)
            {
                if (item.gameObject.CompareTag("Enemy"))
                {
                    item.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
                    Execute(item.gameObject);
                }
            }


        }
        Destroy(gameObject);
    }
}

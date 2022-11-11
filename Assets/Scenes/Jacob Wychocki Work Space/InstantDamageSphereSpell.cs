using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamageSphereSpell : BaseSpell
{
    public float size = 10;
    public GameObject Effect;
    protected override void OnTriggerEnter(Collider other)
    {

    }

    protected override void Update()
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Collider[] Affected = Physics.OverlapSphere(transform.position, size);
        foreach (Collider item in Affected)
        {
            if (item.CompareTag("Enemy"))
            {
                item.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
                Execute(item.gameObject);
            }

        }
        Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}

    // Update is called once per frame
   

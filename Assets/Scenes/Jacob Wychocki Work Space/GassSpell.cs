using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GassSpell : BaseSpell
{
    [SerializeField] float size = 10;
    [SerializeField] float Interval = 1;

    protected override void Start()
    {
        gameObject.transform.position += new Vector3(0, 3, 0);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    float time;
    protected override void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            if (Time.time - time > Interval)
            {
                Duration -= Interval;
                time = Time.time;
                Collider[] Affected = Physics.OverlapSphere(transform.position, size);
                foreach (Collider item in Affected)
                {
                    if (item.CompareTag("Enemy"))
                        item.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
                    Execute(item.gameObject);
                }
            }
            if (Duration < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

    // Start is called before the first frame update
   



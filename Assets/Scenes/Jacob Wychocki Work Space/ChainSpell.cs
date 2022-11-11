using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpell : BaseSpell
{
    Dictionary<GameObject, bool> Affected = new Dictionary<GameObject, bool>();
    public float size = 5;
    public GameObject[] hiteffect;

    // Start is called before the first frame update
    protected override void OnTriggerEnter(Collider other)
    {
        if (IsEnemySpell)
        {

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider>().enabled = false;
            Chain(other.gameObject);
            Destroy(gameObject);
        }
    }
    protected override void Update()
    {
        
    }
    protected void Chain(GameObject chained)
    {
        if (Affected.ContainsKey(chained))
            return;

        Affected.Add(chained, true);

        chained.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
        for (int i = 0; i < hiteffect.Length; i++)
        {
            Instantiate(hiteffect[i], chained.transform.position, transform.rotation);
        }
        Execute(chained);

        Collider[] InRange = Physics.OverlapSphere(chained.transform.position, size);
        foreach (Collider other in InRange)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Chain(other.transform.gameObject);
            }

        }

    }    
}

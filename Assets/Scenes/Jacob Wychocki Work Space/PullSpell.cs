using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PullSpell : BaseSpell   
{
    public float speed = 3;
    public float size = 10;


    public GameObject[] hitEffects;

    protected override void Start()
    {
        Collider[] InRange = Physics.OverlapSphere(transform.position, size);
        foreach (var item in InRange)
        {
            if (item.CompareTag("Enemy"))
                StartCoroutine(PullEnemyToPoint(item.transform));
        }

        foreach (GameObject hitEffect in hitEffects)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }

    }
    protected override void OnTriggerEnter(Collider other)
    {
        
    }

    protected override void Update()
    {
        
    }
    protected virtual IEnumerator PullEnemyToPoint(Transform Enemy)
    {
        float time = Time.time;
        //Enemy.GetComponent<NavMeshAgent>().enabled = false;
        while(Vector3.Distance(Enemy.transform.position, transform.position) > 1.5f)
        {
            if (Time.time - time > 0.5f)
                break;
            Enemy.transform.position = Vector3.Lerp(Enemy.transform.position, transform.position, speed * Time.deltaTime);
            yield return null;
        }
       // Enemy.GetComponent<NavMeshAgent>().enabled = true;
        Enemy.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
        Execute(Enemy.gameObject);
        
    }    

    
    
}
 

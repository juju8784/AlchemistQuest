using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSpell : BaseSpell
{
    [SerializeField]
    protected float BulletSpeed;
    [SerializeField]
    public GameObject hitEffect;

    [SerializeField] float Interval = 1;
    private float time = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        Destroy(gameObject, Duration);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.instance.paused == false)
        {
            transform.position += transform.forward * BulletSpeed * Time.deltaTime;

            time += Time.time;
            if (time > Interval)
            {

            }
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        
        if(IsEnemySpell)
        {

        }
        else if (other.gameObject.CompareTag("Enemy") && other.gameObject != source)
        {
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            other.GetComponent<BaseEnemyController>().TakeDamage(Damage,Type);
            Execute(other.gameObject);
            Destroy(gameObject);
        }
    }

}

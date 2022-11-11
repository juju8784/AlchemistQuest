using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningSpell : BaseSpell
{
    [SerializeField]
    protected float BulletSpeed;
    [SerializeField]
    public GameObject hitEffect;

    [SerializeField] float Interval = 1;
    private float time = 0;
    private GameObject[] hit;
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
            GameManager.instance.HitByLightning.Add(other.gameObject);
            Execute(other.gameObject);
            ChainEffect();
            Destroy(gameObject);
        }
    }

    private void ChainEffect()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 5f);
        if (enemies[0] != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                  
                if (enemies[i].gameObject.CompareTag("Enemy") && CanBeHit(enemies[i].gameObject))
                {
                    transform.LookAt(enemies[i].transform);
                    Vector3 pos = transform.position + transform.forward*2f;
                    GameObject lightning = Instantiate(Resources.Load("AbilityPreFabs/LightningProjectile") as GameObject, pos, Quaternion.identity);
                    lightning.transform.LookAt(enemies[i].transform);
                    break;
                }
            }
        }

    }

    private bool CanBeHit(GameObject enemy)
    {
          
           for (int j = 0; j < GameManager.instance.HitByLightning.Count; j++)
           {
                if (GameManager.instance.HitByLightning[j] == enemy)
                {
                     return false;
                }
           }
        return true;
    }
}

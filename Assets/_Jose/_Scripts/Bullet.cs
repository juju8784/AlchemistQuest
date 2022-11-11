using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float BulletSpeed;
    public float lifeTime;
    public float damage;

    public GameObject hitEffect;

    protected float timer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        OnObjectSpawn();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

        if (other.gameObject.CompareTag("Player") && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            GameManager.instance.playerManager.TakeDamage(damage);
        }
        if (other.gameObject.layer != 11)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            transform.position += transform.forward * BulletSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            if (timer >= lifeTime)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public virtual void OnObjectSpawn()
    {
        timer = 0;
    }
}

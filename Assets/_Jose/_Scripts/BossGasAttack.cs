using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGasAttack : Bullet
{
    public float damageInterval;
    public float explosionDelay;
    public GameObject explosionEffect;

    private List<GameObject> targets;
    private float damageTimer;
    private bool exploding;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targets.Add(other.gameObject);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            transform.position += transform.forward * BulletSpeed * Time.deltaTime;
            if (!exploding)
            {
                timer += Time.deltaTime;
                damageTimer += Time.deltaTime;
                if (timer >= lifeTime)
                {
                    gameObject.SetActive(false);
                    return;
                }

                foreach (GameObject target in targets)
                {
                    target.GetComponent<PlayerManager>().TakeDamage(damage * Time.deltaTime / damageInterval);
                }

                //if (damageTimer >= damageInterval)
                //{
                //    foreach (GameObject target in targets)
                //    {
                //        target.GetComponent<PlayerManager>().TakeDamage(damage);
                //    }
                //    damageTimer = 0;
                //}
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= explosionDelay)
                {
                    GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public override void OnObjectSpawn()
    {
        timer = 0;
        damageTimer = 0;
        exploding = false;
        targets = new List<GameObject>();
        transform.position = new Vector3(transform.position.x,
                                             GameManager.instance.player.transform.position.y,
                                             transform.position.z);
        Vector3 rotation = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(rotation);
        transform.position += new Vector3(0, 1, 0);
    }

    public void Explode()
    {
        exploding = true;
        timer = 0;
    }
}

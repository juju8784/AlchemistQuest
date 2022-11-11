using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionBasicShoot : AIAction
{
    public GameObject bullet;
    public Transform spawnPosition;
    public float bulletSpeed;
    public float damage;
    public float fireRate; // bullets per second
    public float bulletLifetime;

    //Randomness
    public bool useSpread;
    public Vector3 randomRange;

    //Bullet Pool variables
    public bool useBulletPool;
    public string bulletPoolTag;

    protected float timer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isActiveAction && GameManager.instance.paused == false && !controller.isStunned && GameManager.instance.playerManager.dead == false)
        {
            timer += Time.deltaTime;
            if (timer >= 1 / fireRate)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    public virtual void Shoot()
    {
        GameObject obj;
        Quaternion rotation = transform.rotation;

        if (useSpread)
        {
            float xSpread = transform.rotation.eulerAngles.x - randomRange.x / 2 + Random.Range(0, randomRange.x);
            float ySpread = transform.rotation.eulerAngles.y - randomRange.y / 2 + Random.Range(0, randomRange.y);
            float zSpread = transform.rotation.eulerAngles.z - randomRange.z / 2 + Random.Range(0, randomRange.z);
            Vector3 randomRotation = new Vector3(xSpread, ySpread, zSpread);
            rotation = Quaternion.Euler(randomRotation);
        }

        if (!useBulletPool)
        {
            obj = Instantiate(bullet, spawnPosition.position, rotation);
            Destroy(obj, bulletLifetime);
        }
        else
        {
            obj = ObjectPooler.Instance.SpawnFromPool(bulletPoolTag, spawnPosition.position, rotation);
        }
        Bullet settings = obj.GetComponent<Bullet>();
        settings.lifeTime = bulletLifetime;
        settings.damage = damage;
        settings.BulletSpeed = bulletSpeed;
    }

}

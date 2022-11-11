using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPosition;
    public float bulletSpeed;
    public float fireRate; // bullets per second

    private float timer;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude < 0.15f && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            timer += Time.deltaTime;
            if (timer >= 1 / fireRate)
            {
                GameObject bull = Instantiate(bullet, spawnPosition.position, transform.rotation);
                bull.GetComponent<Bullet>().BulletSpeed = bulletSpeed;
                timer = 0;
            }
        }
    }
}

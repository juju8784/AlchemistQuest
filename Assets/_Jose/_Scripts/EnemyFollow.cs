using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        if (agent.velocity.magnitude < 0.15f && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            Vector3 playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);

            Quaternion _lookRotation =
            Quaternion.LookRotation((playerPos - transform.position).normalized);

            transform.rotation = _lookRotation;
        }
        MeleeAttack();
    }

    void MeleeAttack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 2 && GameManager.instance.playerManager.dead == false)
        {
            transform.position = this.transform.position;
            anim.SetBool("Attacking", true);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) >= 2.2 && GameManager.instance.playerManager.dead == false)
            anim.SetBool("Attacking", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CentipedeController : BaseEnemyController
{
    protected List<GameObject> bodyParts;

    protected override void Start()
    {
        base.Start();

        //Adds all of the body parts to the centipede
        foreach (Transform child in transform)
        {
            NavMeshAgent bodyAgent = child.gameObject.GetComponent<NavMeshAgent>();
            if (bodyAgent == null) continue;
            bodyAgent.acceleration = agent.acceleration;
            bodyAgent.angularSpeed = agent.angularSpeed;
            bodyAgent.speed = agent.speed;
            bodyParts.Add(child.gameObject);
        }
    }

    protected override void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            for (int i = 0; i < bodyParts.Count; i++)
            {
                NavMeshAgent bodyAgent = bodyParts[i].GetComponent<NavMeshAgent>();
                bodyAgent.isStopped = agent.isStopped;
                bodyAgent.speed = agent.speed;
                if (i > 0)
                    bodyAgent.SetDestination(bodyParts[i - 1].transform.position);
                else
                    bodyAgent.SetDestination(transform.position);
            }
            base.Update();
        }
    }
}

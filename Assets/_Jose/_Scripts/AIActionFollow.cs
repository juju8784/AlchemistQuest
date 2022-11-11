using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AI;

public class AIActionFollow : AIAction
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        //if (isActiveAction && GameManager.instance.paused == false)
        //{
        //    RunAction();
        //}
    }

    public override void RunAction()
    {
        if (!controller.alwaysFollowPlayer)
        {
            NavMeshPath path = new NavMeshPath();
            controller.agent.CalculatePath(controller.target.position, path);
            controller.agent.SetPath(path);
        }

        if (controller.agent.velocity.magnitude < 0.15f)
        {
            Vector3 playerPos = new Vector3(controller.target.position.x, transform.position.y, controller.target.position.z);

            Quaternion _lookRotation =
            Quaternion.LookRotation((playerPos - transform.position).normalized);

            transform.rotation = _lookRotation;
        }
    }
}

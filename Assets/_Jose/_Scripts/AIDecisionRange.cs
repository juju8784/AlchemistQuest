using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionRange : AIDecision
{
    public float detectionRange;
    [ReadOnly] public float remaining;
    public override bool RunDecision()
    {
        if (controller.agent)
        {
            if (controller.agent.pathPending)
            {
                remaining = Vector3.Distance(transform.position, controller.target.position);
            }
            else
                remaining = controller.agent.remainingDistance;
            if (remaining <= detectionRange)
            {
                CurrentValue = true;
            }
            else
            {
                CurrentValue = false;
            }
            return CurrentValue;
        }
        else
        {
            remaining = Vector3.Distance(transform.position, controller.target.position);
            if (remaining <= detectionRange)
            {
                CurrentValue = true;
            }
            else
            {
                CurrentValue = false;
            }
            return CurrentValue;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionFollowing : AIDecision
{
    public override bool RunDecision()
    {
        if (controller.agent.velocity.magnitude < 0.15f &&
            controller.agent.stoppingDistance >= controller.agent.remainingDistance)
        {
            CurrentValue = false;
        } 
        else
            CurrentValue = true;

        

        return CurrentValue;
    }
}

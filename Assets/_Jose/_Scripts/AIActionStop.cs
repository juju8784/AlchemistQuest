using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionStop : AIAction
{
    public override void RunAction()
    {
        controller.agent.isStopped = true;
    }
}

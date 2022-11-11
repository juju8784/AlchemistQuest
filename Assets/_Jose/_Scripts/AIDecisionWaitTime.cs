using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This decision will have a value of false until the specified
/// wait time has been reached
/// </summary>
public class AIDecisionWaitTime : AIDecision
{
    public float waitTime;
    private float timer;

    protected override void Start()
    {
        base.Start();
        timer = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
            timer += Time.deltaTime;
    }

    public override bool RunDecision()
    {
        if (timer >= waitTime)
            CurrentValue = true;
        else
            CurrentValue = false;
        return CurrentValue;
    }

    public override void OnActiveChange()
    {
        timer = 0f;
    }
}

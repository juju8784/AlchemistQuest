using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This will set the designated bool parameter to true
public class AIActionAnimationChange : AIAction
{
    public Animator animator;
    public string parameterName;

    //Set this to true to use setBool
    //Set this to false to use setTrigger
    public bool useBool = false;

    private bool triggerSet = false;

    protected override void Start()
    {
        base.Start();
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    public override void RunAction()
    {
        if (animator)
        {
            if (useBool)
                animator.SetBool(parameterName, true);
            else if (!triggerSet)
            {
                animator.SetTrigger(parameterName);
                triggerSet = true;
            }
        }
            
    }

    public override void OnActiveChange()
    {
        if (animator)
            if (useBool)
                animator.SetBool(parameterName, false);

        triggerSet = false;
    }
}

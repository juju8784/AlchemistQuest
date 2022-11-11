using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//This is the core of the state machine
public class AIState : MonoBehaviour
{
    public string stateName;

    [ReadOnly] public bool isActive = false;

    //Animation Changes
    public string animBool;

    public bool isActiveState {
        get { return isActive; }
        set { isActive = value; }
    }

    public string[] actionNames;
    public Decision[] decisions;

    protected BaseEnemyController controller;


    private void OnGUI()
    {
        controller = GetComponent<BaseEnemyController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<BaseEnemyController>();
        }
    }

    private void Awake()
    {
        controller = GetComponent<BaseEnemyController>();
    }

    //Will activate this state along with all of its actions and decisions
    //You can also deactivate them by using false as the parameter
    public virtual void ActivateState(bool boolState = true)
    {
        isActiveState = boolState;

        //Animation Changes
        if (controller.anim != null && animBool.Length > 0)
        {
            controller.anim.SetBool(animBool, boolState);
        }

        for (int i = 0; i < actionNames.Length; i++)
        {
            controller.ActivateAction(actionNames[i], boolState);
        }

        for (int i = 0; i < decisions.Length; i++)
        {
            controller.ActivateDecision(decisions[i].decisionName, boolState);
        }
    }
}


//Used for holding the decisions
//Leave either the true or false state blank to stay in the current state
[Serializable]
public class Decision 
{
    public string decisionName;

    //The state that the controller will be directed to if true
    public string trueState;

    //The state that the controller will be directed to if false
    public string falseState;

    public Decision()
    {
        decisionName = "";
        trueState = "";
        falseState = "";
    }

    public Decision(string dn, string ts, string fs)
    {
        decisionName = dn;
        trueState = ts;
        falseState = fs;
    }
}

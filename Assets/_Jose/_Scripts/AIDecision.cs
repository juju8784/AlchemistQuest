using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecision : MonoBehaviour
{
    public string DecisionName;

    [SerializeField] private bool isActive = false; 
    public bool isActiveDecision
    {
        get { return isActive; }
        set 
        { 
            isActive = value;
            OnActiveChange();
        }
    }
    protected BaseEnemyController controller;

    //Keeps the current value of the decision
    [ReadOnly] public bool CurrentValue;


    private void OnGUI()
    {
        controller = GetComponent<BaseEnemyController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<BaseEnemyController>();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        controller = GetComponent<BaseEnemyController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    //Runs the decision, then returns the outcome
    public virtual bool RunDecision()
    {
        return CurrentValue;
    }

    public virtual void OnActiveChange()
    {

    }
}

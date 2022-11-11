using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAction : MonoBehaviour
{
    public string ActionName;

    [SerializeField] private bool isActive = false;
    public bool isActiveAction
    {
        get { return isActive; }
        set
        {
            isActive = value;
            OnActiveChange();
        }
    }
    protected BaseEnemyController controller;

    private void OnGUI()
    {
        controller = GetComponent<BaseEnemyController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<BaseEnemyController>();
        }
        
    }

    protected virtual void Awake()
    {

    }

    protected virtual void Start() 
    {
        controller = GetComponent<BaseEnemyController>();
    }

    
    protected virtual void Update()
    {
        //Override this if you want it to happen always
    }

    //Runs the action
    public virtual void RunAction()
    {
        //Override this method if you want the action to be uninterruptable
    }

    public virtual void OnActiveChange()
    {

    }
}

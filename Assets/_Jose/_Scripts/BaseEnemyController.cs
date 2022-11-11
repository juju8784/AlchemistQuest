using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BaseEnemyController : MonoBehaviour
{
    public Abilities Type = Abilities.NULL;
    public float maxEnemyHealth = 100;
    public float enemyHealth = 100;
    public GameObject healthBar;
    protected Image healthBarFill;
    //public GameObject resistanceEffect;

    public GameObject itemDrop;
    protected Vector3 startPosition;
    public GameObject damageEffect;
    public GameObject hitSound;

    //Check this to always have the destination be the player
    public bool alwaysFollowPlayer;

    public string StartingStateName;
    //Keeps track of the current state
    [ReadOnly] public string currentStateName;
    protected AIState currentState;

    //Speed variables
    public float OriginalSpeed { get; set; }
    [ReadOnly] public float currentSpeed;
    public int maxSlowStacks;
    public float slowPercentage;
    protected int slowStacks;
    
    public bool IsFlammable = false;

    // Stun
    public bool isStunned = false;

    public NavMeshAgent agent { get; set; }
    public Transform target;

    public Animator anim;

    //Despawn
    public float despawnRange = 100;

    //Lists of the different states, actions, and decisions
    protected List<AIState> states;
    protected List<AIAction> actions;
    protected List<AIDecision> decisions;

    public virtual void TakeDamage(float damage, Abilities Attacker)
    {
        if (damageEffect)
        {  // bleeding particle system
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            //if(resistanceEffect)
            enemyHealth -= damage * ModiferDamage.ReturnDamageModifier(Attacker, Type, IsFlammable);
        }

        if (hitSound)
        {
            hitSound.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
            hitSound.GetComponent<AudioSource>().Play();
        }

        if (healthBarFill)
        {
            healthBarFill.fillAmount = enemyHealth / maxEnemyHealth;
        }

        if(enemyHealth <= 0)
        {
            if (anim)
            {
                isStunned = true;
                StartCoroutine(DeathAnimationWaitTime());
            }
            else
            {
                Death();
            }
        }
    }

    public virtual IEnumerator DeathAnimationWaitTime()
    {
        yield return new WaitForSeconds(1.4f);
        Death();
    }
    public virtual void Death()
    {
        if(itemDrop)
        Instantiate(itemDrop, transform.position, itemDrop.transform.rotation);

        gameObject.SetActive(false);
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
        //enemyHealth = maxEnemyHealth;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (healthBar)
        {
            healthBar = Instantiate(healthBar, GameManager.instance.EnemyHealthBars.transform);
            healthBarFill = healthBar.GetComponent<Image>();
        }
        startPosition = transform.position;
        
        agent = GetComponent<NavMeshAgent>();

        //Handles the animator
        if (!anim)
            anim = GetComponent<Animator>();

        //Sets the target to player if none is selected
        if (alwaysFollowPlayer || target == null)
            target = GameManager.instance.player.transform;
        if(agent)
        agent.SetDestination(target.position);
        OriginalSpeed = agent.speed;

        #region States, Actions, Decisions Setup
        states = new List<AIState>();
        actions = new List<AIAction>();
        decisions = new List<AIDecision>();

        AIState[] stateArray = GetComponents<AIState>();
        AIAction[] actionArray = GetComponents<AIAction>();
        AIDecision[] decisionArray = GetComponents<AIDecision>();

        for (int i = 0; i < stateArray.Length; i++)
            AddState(stateArray[i]);

        for (int i = 0; i < actionArray.Length; i++)
            AddAction(actionArray[i]);

        for (int i = 0; i < decisionArray.Length; i++)
            AddDecision(decisionArray[i]);

        #endregion

        //Sets the starting state and activates it
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].stateName.Equals(StartingStateName))
            {
                currentStateName = states[i].stateName;
                currentState = states[i];
                states[i].ActivateState();
                return;
            }
        }
    }

    //This will run all of the actions and decisions based on the current state
    protected virtual void Update()
    {
        if (GameManager.instance.paused == false && !isStunned && CheckActivationRange() && GameManager.instance.playerManager.dead == false)
        {
            if(agent)
                agent.isStopped = false;
            if (alwaysFollowPlayer)
            {
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(target.position, path);
                agent.SetPath(path);
            }

            float slowSpeed = CalculateSlow();
            currentSpeed = OriginalSpeed - slowSpeed;
            if(agent)
                agent.speed = currentSpeed;
            RunAllActions();
            RunAllDecisions();

            //Changes the state if necessary
            for (int i = 0; i < currentState.decisions.Length; i++)
            {
                AIDecision currentDecision = GetDecision(currentState.decisions[i].decisionName);
                // Grabs the state name
                string inputState = "";
                if (currentDecision.CurrentValue)
                    inputState = currentState.decisions[i].trueState;
                else
                    inputState = currentState.decisions[i].falseState;

                //If the state name is nothing, continues if false
                if (inputState.Equals("")) continue;

                bool success = SwitchState(inputState);
                if (success)
                    break;
            }
        }
        else
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
        if(healthBar)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + (new Vector3(0, 1, 1)));
    }

    //Calculates the amount of slow to apply
    public float CalculateSlow()
    {
        float slow = slowStacks * slowPercentage * 0.01f * OriginalSpeed;

        if (slow > OriginalSpeed)
            return OriginalSpeed;
        return slow;
    }

    //Attempts to add a slow stack and then returns its success
    public bool AddSlowStack()
    {
        if (slowStacks >= maxSlowStacks)
            return false;
        slowStacks++;
        return true;
    }

    //Attempts to remove a slow stack and then returns its success;
    public bool RemoveSlowStack()
    {
        if (slowStacks <= 0)
            return false;
        slowStacks--;
        return true;
    }

    public bool CheckActivationRange()
    {
        if (Vector3.Distance(transform.position, target.position) >= despawnRange)
        {
            return false;
        }
        return true;
    }

    #region Helper Functions

    //Adds a state to the states list
    public void AddState(AIState state)
    {
        if (!states.Contains(state))
            states.Add(state);
    }

    //Adds an action to the action list
    public void AddAction(AIAction action)
    {
        if (!actions.Contains(action))
            actions.Add(action);
    }

    //Adds a decision to the decision list
    public void AddDecision(AIDecision decision)
    {
        if (!decisions.Contains(decision))
            decisions.Add(decision);
    }

    public void ResetAll()
    {
        for (int i = 0; i < states.Count; i++)
            states[i].ActivateState(false);
        for (int i = 0; i < actions.Count; i++)
            ActivateAction(actions[i].ActionName, false);
        for (int i = 0; i < decisions.Count; i++)
            ActivateDecision(decisions[i].DecisionName, false);
    }

    //Runs the specified action if the name exists
    public void RunAction(string actionName)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].ActionName.Equals(actionName))
            {
                actions[i].RunAction();
            }
        }
    }

    //Runs the specified decision if the name exists
    public void RunDecision(string decisionName)
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            if (decisions[i].DecisionName.Equals(decisionName))
            {
                decisions[i].RunDecision();
                return;
            }
        }
    }

    //Runs all active actions
    public void RunAllActions()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].isActiveAction)
                actions[i].RunAction();
        }
    }

    //Runs all active decisions
    public void RunAllDecisions()
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            if (decisions[i].isActiveDecision)
                decisions[i].RunDecision();
        }
    }

    //Returns the value of the decision. If it doesn't find the decision, it returns false
    public bool CheckDecisionResult(string decisionName)
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            if (decisions[i].DecisionName.Equals(decisionName))
            {
                return decisions[i].CurrentValue;
            }
        }

        Debug.Log("CheckDecisionResult FAILED: Invalid DecisionName");
        return false;
    }

    //Switches the current state to stateName. If it succeeds,
    //it returns true and false otherwise
    public bool SwitchState(string stateName)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].stateName.Equals(stateName))
            {
                currentState.ActivateState(false);
                states[i].ActivateState();
                currentStateName = states[i].stateName;
                currentState = states[i];
                return true;
            }
        }
        return false;
    }


    //Activates the specified action. You can also specify a bool to deactivate
    public void ActivateAction(string actionName, bool boolState)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].ActionName.Equals(actionName))
                actions[i].isActiveAction = boolState;
        }
    }

    //Activates the specified decision. You can also specify a bool to deactivate
    public void ActivateDecision(string decisionName, bool boolState = true)
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            if (decisions[i].DecisionName.Equals(decisionName))
                decisions[i].isActiveDecision = boolState;
        }
    }

    //Takes in the action name and returns the AIAction if it exists
    public AIAction GetAction(string actionName)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].ActionName.Equals(actionName))
                return actions[i];
        }
        return null;
    }

    //Takes in the decision name and returns the AIDecision if it exists
    public AIDecision GetDecision(string decisionName)
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            if (decisions[i].DecisionName.Equals(decisionName))
                return decisions[i];
        }
        return null;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GolemController : BaseEnemyController
{
    public override void TakeDamage(float damage, Abilities Attacker)
    {
        if (damageEffect)
        {  // bleeding particle system
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            //if(resistanceEffect)
            enemyHealth -= damage * ModiferDamage.ReturnDamageModifier(Attacker, Type, IsFlammable);
        }

        if (healthBarFill)
        {
            healthBarFill.fillAmount = enemyHealth / maxEnemyHealth;
        }

        if (enemyHealth <= 0)
        {
            if (anim)
            {
                anim.SetBool("Death", true);
                isStunned = true;
                StartCoroutine(DeathAnimationWaitTime());
            }
            else
            {
                Death();
            }
        }
    }

    public override IEnumerator DeathAnimationWaitTime()
    {
        yield return new WaitForSeconds(2f);
        Death();
    }

    protected override void Start()
    {
        if (healthBar)
        {
            healthBar = Instantiate(healthBar, GameManager.instance.EnemyHealthBars.transform);
            healthBarFill = healthBar.GetComponent<Image>();
        }
        startPosition = transform.position;

        if (!agent)
            agent = GetComponent<NavMeshAgent>();

        //Sets the target to player if none is selected
        if (alwaysFollowPlayer || target == null)
            target = GameManager.instance.player.transform;
        currentSpeed = agent.speed;
        OriginalSpeed = agent.speed;

        //Handles the animator
        if (!anim)
            anim = GetComponent<Animator>();

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

    protected override void Update()
    {
        if (GameManager.instance.paused == false 
            && !isStunned && CheckActivationRange() 
            && GameManager.instance.playerManager.dead == false
            && enemyHealth > 0)
        {
            agent.isStopped = false;
            if (alwaysFollowPlayer)
            {
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(target.position, path);
                agent.SetPath(path);
            }

            //Speed
            float slowSpeed = CalculateSlow();
            currentSpeed = OriginalSpeed - slowSpeed;
            agent.speed = currentSpeed;
            anim.speed = currentSpeed / OriginalSpeed;

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
            anim.speed = 0;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }

        if (healthBar)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + (new Vector3(0, 5, 1)));

        if (enemyHealth <= 0)
        {
            anim.speed = 1;
            isStunned = true;
        }
    }
}

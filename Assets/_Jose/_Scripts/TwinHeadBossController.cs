using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TwinHeadBossController : BaseEnemyController
{
    private Transform parent;
    public Transform pivot;

    private bool damageStunned = false;
    private float timer = 0;
    private int stunsLeft = 3;

    protected override void Start()
    {
        currentSpeed = 0;
        healthBarFill = healthBar.GetComponent<Image>();
        startPosition = transform.position;
        parent = transform.parent;

        //Sets the target to player if none is selected
        if (alwaysFollowPlayer || target == null)
            target = GameManager.instance.player.transform;

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

    public override void TakeDamage(float damage, Abilities Attacker)
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

        if (enemyHealth <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        anim.SetBool("isDead", true);
    }
    
    protected override void Update()
    {

        if (enemyHealth <= 0)
        {
            anim.speed = 1;
            return;
        }
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            anim.speed = 1;
            //Rotates the parent transform to the player
            if (enemyHealth > 0)
            {
                Vector3 playerPos = new Vector3(target.position.x, pivot.position.y, target.position.z);

                Quaternion _lookRotation =
                Quaternion.LookRotation((playerPos - pivot.position).normalized);

                pivot.rotation = _lookRotation;
                pivot.Rotate(-15f, 0, 0);
            }


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
        }

    }

}

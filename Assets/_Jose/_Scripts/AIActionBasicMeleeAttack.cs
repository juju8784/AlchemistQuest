using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionBasicMeleeAttack : AIAction
{
    public float lifetime;
    public float attackSpeed;
    public GameObject meleeAttack;
    public Transform position;

    private float timer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if(GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        timer += Time.deltaTime;
    }

    public override void RunAction()
    {
        if (timer >= 1 / attackSpeed)
        {
            GameObject attack = Instantiate(meleeAttack, position.position, transform.rotation);
            Destroy(attack, lifetime);
            timer = 0;
        }
    }


    public override void OnActiveChange()
    {
        timer = 0;
    }
}

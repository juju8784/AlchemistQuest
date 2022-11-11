using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNeckController : BaseEnemyController
{
    public TwinHeadBossController controller;
    public override void TakeDamage(float damage, Abilities Attacker)
    {
        if (controller.enemyHealth > 0)
            controller.TakeDamage(damage/4, Attacker);
    }

    protected override void Start()
    {
    }

    protected override void Update()
    {
    }

    public override void Death()
    {
        
    }
}

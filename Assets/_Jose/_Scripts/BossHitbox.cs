using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    public TwinHeadBossController controller;

    private void OnTriggerEnter(Collider other)
    {
        BaseSpell spell = other.GetComponent<BaseSpell>();
        if (spell)
        {
            controller.TakeDamage(spell.Damage, Abilities.NULL);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int damage = 15;

    public GameObject startingEffect;
    public GameObject[] hitEffects;


    private void Start()
    {
        GameObject effect = Instantiate(startingEffect, transform.position, transform.rotation);
        effect.transform.localScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject hitEffect in hitEffects)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
                effect.transform.localScale = transform.localScale;
            }
            GameManager.instance.playerManager.TakeDamage(damage);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollision : MonoBehaviour
{
    public float damage;
    private GameManager GM;

    void Start()
    {
        GM = GameManager.instance;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            GM.playerManager.TakeDamage(damage);
        }
    }

   
}

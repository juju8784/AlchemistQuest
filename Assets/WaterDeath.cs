using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            GameManager.instance.playerManager.TakeDamage(9999);
        }
    }
}

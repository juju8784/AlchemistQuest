using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this on a separate gameobject with a trigger collider
/// to use this script
/// </summary>
public class DamageOnTouch : MonoBehaviour
{
    public float damage;
    public float hitInterval;
    public GameObject hitEffect;

    private float timer;
    private bool timerStarted;

    private void Start()
    {
        timer = hitInterval;
        timerStarted = false;
    }

    //private void OnTriggerStay(Collider collider)
    //{
    //    if (collider.gameObject.CompareTag("Player") && !GameManager.instance.paused)
    //    {
    //        timer += Time.deltaTime;

    //        if (timer >= hitInterval)
    //        {
    //            GameManager.instance.playerManager.TakeDamage(damage);
    //            if (hitEffect)
    //                Instantiate(hitEffect, transform.position, transform.rotation);
    //            timer = 0;
    //        }
    //        return;
    //    }
    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !GameManager.instance.paused && GameManager.instance.playerManager.dead == false)
        {
            timerStarted = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !GameManager.instance.paused && GameManager.instance.playerManager.dead == false)
        {
            timerStarted = false;
            timer += 0.5f;
        }
    }

    private void Update()
    {
        if (timerStarted && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            timer += Time.deltaTime;
            if (timer >= hitInterval)
            {
                GameManager.instance.playerManager.TakeDamage(damage);
                if (hitEffect)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                timer = 0;
            }
        }
    }
}

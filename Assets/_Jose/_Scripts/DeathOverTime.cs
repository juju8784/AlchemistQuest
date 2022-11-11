using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOverTime : MonoBehaviour
{
    public float lifetime;
    public bool Deactivate;

    private float timer;

    private void Start()
    {
        if (!Deactivate)
            Destroy(gameObject, lifetime);
        
    }

    private void Update()
    {
        if (Deactivate && GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            timer += Time.deltaTime;

            if (timer >= lifetime)
                gameObject.SetActive(false);
        }    
    }
}

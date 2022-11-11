using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarManager : MonoBehaviour
{
    [SerializeField]
    private Image HealthBarImage = null;
    [SerializeField]
    private float Health { get; set; }
    [SerializeField]
    public float maxHealth { get; set; }


    public void UpdateHealthUI()
    {
        HealthBarImage.fillAmount = Health / maxHealth;
        if(HealthBarImage.fillAmount < .33)
        {
            HealthBarImage.color = Color.red;
        }
        else if (HealthBarImage.fillAmount < .66)
        {
            HealthBarImage.color = Color.yellow;
        }
        else
        {
            HealthBarImage.color = Color.green;
        }

    }
    public void SetHealth(float setHealth)
    {
        Health = Mathf.Clamp(setHealth, 0, maxHealth);
        UpdateHealthUI();
    }
    public float GetHealth()
    {
        return Health;
    }


}

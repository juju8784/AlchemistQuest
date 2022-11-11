using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{


    void Start()
    {
        PlayerPrefs.GetInt("AcidAbility", 0);

    }

    void IncreaseAcidAbility(int amount)
    {
        PlayerPrefs.SetInt("AcidAbility", (PlayerPrefs.GetInt("AcidAbility", 0) + amount));

        // :D
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseAcidAbility(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
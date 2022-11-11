using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeathBarManager : MonoBehaviour
{
    private BaseEnemyController controller;
    [SerializeField] private Image HealthBarImage = null;
    


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<BaseEnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        HealthBarImage.fillAmount = controller.enemyHealth / controller.maxEnemyHealth;
        if (HealthBarImage.fillAmount < .33)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushTrap : MonoBehaviour
{
    private SphereCollider col;

    public float radius;
    public bool isActive;
    public int spawnEnemies;


    void Start()
    {
        col = GetComponent<SphereCollider>();
        col.radius = radius;
        isActive = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }

    private void SpawnEnemies()
    {

    }

}

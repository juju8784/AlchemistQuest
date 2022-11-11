using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightMinionsSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnLocations = null;
    [SerializeField] GameObject enemy = null;
    private bool started = false;
    public int WavesOfEnemies = 1;
    public float SpawnDelay;

    private int WavesSpawnedCount;
    private float timer;
    

    private IEnumerator couroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnEnemies();
            started = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        if (started)
        {
            if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
            {
                if (started == true && WavesSpawnedCount < WavesOfEnemies)
                {
                    timer += Time.deltaTime;
                    if (timer >= SpawnDelay)
                    {
                        SpawnEnemies();
                        timer = 0;
                    }
                }
            }
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            
            Instantiate(enemy, spawnLocations[i].transform.position, Quaternion.identity);
            
        }
        WavesSpawnedCount++;
    }
}

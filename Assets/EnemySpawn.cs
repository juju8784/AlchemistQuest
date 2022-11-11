using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    [SerializeField] GameObject[] spawnLocations = null;
    public int EnemiesPerSpawn = 1;
    [SerializeField] GameObject enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                for (int j = 0; j < EnemiesPerSpawn; j++)
                {
                    //Invoke("SpawnEnemy", j);
                    Instantiate(enemy, spawnLocations[i].transform.position + new Vector3(j*1.5f,0,0), Quaternion.identity);
                }
            }
            //for (int i = 0; i < spawnLocations.Length; i++)
            //{
            //    Destroy(spawnLocations[i].gameObject);
            //}
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

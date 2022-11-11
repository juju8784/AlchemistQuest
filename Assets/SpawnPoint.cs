using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject spawnpoint = null;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;

    private void Start()
    {
        //spawnpoint.transform.position = GameManager.instance.player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnpoint.transform.position = gameObject.transform.position + new Vector3(xOffset, yOffset, zOffset);

        }
    }
}

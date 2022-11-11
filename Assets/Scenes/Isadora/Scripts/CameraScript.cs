using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] Transform target;
    Vector3 playerPosition;

    public float Height = 15;
    public float z_Offset = -4;

    void Start()
    {
        target = GameManager.instance.player.transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        playerPosition = GameManager.instance.player.transform.position + new Vector3(0, Height, z_Offset);
        Vector3 velocity = Vector3.zero;
        transform.position = playerPosition; // Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, 0.03f);
    }
}

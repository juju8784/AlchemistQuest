using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    BoxCollider col;
    Animator anim;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        anim = GetComponentInParent<Animator>();
        Check();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("openGate", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("openGate", false);
        }
    }

    void Check()
    {
        if (col == null)
        {
            Debug.Log("collider is null");
        }
        if (anim == null)
        {
            Debug.Log("animator is null");
        }
    }
   
}

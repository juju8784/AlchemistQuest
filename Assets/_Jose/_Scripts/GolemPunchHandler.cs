using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPunchHandler : MonoBehaviour
{
    public float lifetime;
    public GameObject meleeAttack;
    public Transform position;

    public void Punch()
    {
        GameObject attack = Instantiate(meleeAttack, position.position, transform.rotation);
        Destroy(attack, lifetime);
    }
}

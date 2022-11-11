using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSpell : IceSpell
{
    //Only for Player
    private GameObject attached;
    protected override void Start()
    {
        attached = GameManager.instance.player;
    }

    protected override void Update()
    {
        base.Update();
        transform.position = attached.transform.position;
    }

    // Start is called before the first frame update
    
}

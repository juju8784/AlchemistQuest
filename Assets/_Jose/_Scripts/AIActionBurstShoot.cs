using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionBurstShoot : AIActionBasicShoot
{
    public int bulletsPerBurst;
    public float burstFireRate;

    private float burstTimer;
    private int bulletsShot;
    private bool inBurst;


    protected override void Update()
    {
        base.Update();
    }

    public override void RunAction()
    {
        //if (!inBurst)
        //{
        //    timer += Time.deltaTime;
        //    if (timer >= 1 / fireRate)
        //    {
        //        inBurst = true;
        //        timer = 0;
        //    }
        //}
        //else
        //{
        //    burstTimer += Time.deltaTime;
        //    if (burstTimer >= 1 / burstFireRate)
        //    {
        //        Shoot();
        //        bulletsShot++;
        //        burstTimer = 0;
        //        if (bulletsShot >= bulletsPerBurst)
        //        {
        //            inBurst = false;
        //        }
        //    }
        //}
    }
}

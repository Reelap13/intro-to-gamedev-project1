using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAttack : Attack
{
    public override void PerformAttack()
    {
        GetComponent<Magazine>().ReduceAmmo(1);
        var projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        projectile.GetComponent<Rigidbody>().AddForce(launchPoint.forward * force, forceMode);
    }
}

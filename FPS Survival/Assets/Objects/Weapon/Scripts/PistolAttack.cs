using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAttack : Attack
{
    public override void PerformAttack()
    {
        var projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);

        projectile.GetComponent<Rigidbody>().AddForce(launchPoint.forward * force, forceMode);
    }
}

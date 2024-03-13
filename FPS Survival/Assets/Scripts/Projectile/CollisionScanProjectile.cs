using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScanProjectile : Projectile
{
    public override void OnTargetCollision(Collision collision)
    {
        collision.gameObject.GetComponent<HealthSystem>().TakeDamage(Damage);
    }
}

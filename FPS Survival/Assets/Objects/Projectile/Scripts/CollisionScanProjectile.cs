using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScanProjectile : Projectile
{
    public override void OnTargetCollision(Collision collision)
    {
        //collision.gameObject.GetComponent<HealthSystem>().TakeDamage(Damage);
        IWeaponVisitor visitor;
        if (collision.gameObject.TryGetComponent(out visitor))
        {
            visitor.Visit(this);
            return;
        }
            
        visitor = GetComponentInParent<IWeaponVisitor>();
        if (visitor != null)
            visitor.Visit(this);
    }
}

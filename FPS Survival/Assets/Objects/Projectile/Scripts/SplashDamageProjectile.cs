using UnityEngine;

public class SplashDamageProjectile : Projectile
{
    [SerializeField] private float radius;
    public override void OnTargetCollision(Collision collision)
    {
        //collision.gameObject.GetComponent<HealthSystem>().TakeDamage(Damage);
        IWeaponVisitor visitor;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.TryGetComponent(out visitor))
            {
                visitor.Visit(this);
            }
            else continue;
        }

        visitor = GetComponentInParent<IWeaponVisitor>();
        if (visitor != null)
            visitor.Visit(this);
    }
}

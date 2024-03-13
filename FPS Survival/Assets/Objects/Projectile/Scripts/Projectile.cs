using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{

    [SerializeField] private int damage = 10;
    [SerializeField] private ProjectileDisposalType type = ProjectileDisposalType.OnAnyCollision;
    [SerializeField] private Rigidbody projectileRigidbody;

    private bool isDisposed;

    [SerializeField] private bool spawnEffectOnDestroy = true;
    [SerializeField] private ParticleSystem effectOnDestroyPrefab;
    [SerializeField] private float effectOnDestroyLifetime = 2f;


    public int Damage => damage;
    public ProjectileDisposalType Type => type;
    public Rigidbody Rigidbody => projectileRigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag + " " + isDisposed);
        if (isDisposed) return;

        if (collision.transform.CompareTag("Enemy"))
        {
            OnTargetCollision(collision);

            if(type == ProjectileDisposalType.OnTargetCollision)
            {
                DisposeProjectile();
            }
        }
        else
        {
            OnOtherCollision(collision);
        }

        OnAnyCollision(collision);

        if(type == ProjectileDisposalType.OnAnyCollision)
        {
            DisposeProjectile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisposeProjectile()
    {
        OnProjectileDispose();

        SpawnEffectOnDestroy();

        Destroy(gameObject);

        isDisposed = true;
    }

    private void SpawnEffectOnDestroy()
    {
        if (spawnEffectOnDestroy == false) return;

        var effect = Instantiate(effectOnDestroyPrefab, transform.position, Quaternion.identity);

        Destroy(effect.gameObject, effectOnDestroyLifetime);
    }

    public virtual void OnProjectileDispose() { }
    public virtual void OnAnyCollision(Collision collision) { }
    public virtual void OnOtherCollision(Collision collision) { }
    public virtual void OnTargetCollision(Collision collision) { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{

    [SerializeField] private int damage = 10;
    [SerializeField] private ProjectileDisposalType type = ProjectileDisposalType.OnAnyCollision;
    [SerializeField] private Rigidbody projectileRigidbody;
    [SerializeField] private float projectileLifetime = 1f;

    private bool isDisposed;

    [SerializeField] private bool spawnEffectOnDestroy = true;
    [SerializeField] private ParticleSystem effectOnDestroyPrefab;
    [SerializeField] private float effectOnDestroyLifetime = 2f;

    [SerializeField] private AudioClip explosion;


    public int Damage => damage;
    public ProjectileDisposalType Type => type;
    public Rigidbody Rigidbody => projectileRigidbody;

    private void Start()
    {
        Invoke(nameof(DisposeProjectile), projectileLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
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
        effect.Play();
        effect.gameObject.GetComponent<AudioSource>().clip = explosion;
        effect.gameObject.GetComponent<AudioSource>().Play();

        Destroy(effect.gameObject, effectOnDestroyLifetime);
    }

    public virtual void OnProjectileDispose() { }
    public virtual void OnAnyCollision(Collision collision) { }
    public virtual void OnOtherCollision(Collision collision) { }
    public virtual void OnTargetCollision(Collision collision) { }
}

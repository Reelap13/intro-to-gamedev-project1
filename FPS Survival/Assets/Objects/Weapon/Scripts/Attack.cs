using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected Transform launchPoint;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected ForceMode forceMode = ForceMode.Impulse;
    [SerializeField] protected float force = 10f;


    public virtual void PerformAttack() { }
}

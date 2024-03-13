using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HealthSystem : MonoBehaviour, IWeaponVisitor
{
   [NonSerialized] public UnityEvent<int> OnHealthChanged = new UnityEvent<int>();

    public int MaxHealth
    {
        get => maxHealth;

        set => maxHealth = value > 0 ? value : 0;
        
    }

    public int Health
    {
        get => health;

        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged.Invoke(health);
        }
    }

    private int health;
    [SerializeField] private int maxHealth;

    public void Awake()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log(Health);
    }

    public void Visit(EnemyMakingDamage visitor)
    {
        TakeDamage(Convert.ToInt32(visitor.Damage));
    }

    public void Visit(CollisionScanProjectile visitor)
    {
        return;
    }
}

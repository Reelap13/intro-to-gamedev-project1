using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{

    public Action<int> OnHealthChanged;

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
            OnHealthChanged?.Invoke(health);
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
}

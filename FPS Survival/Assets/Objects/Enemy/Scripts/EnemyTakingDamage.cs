using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTakingDamage : MonoBehaviour, IWeaponVisitor
{
    [NonSerialized] public UnityEvent OnDieing = new UnityEvent();
    [NonSerialized] public UnityEvent<float> OnTakingDamage = new UnityEvent<float>();
    [NonSerialized] public UnityEvent<float> OnChangingHealth = new UnityEvent<float>();
    [SerializeField] private float _start_health = 10f;

    private float _health;

    private void Awake()
    {
        _health = _start_health;
    }

    public void Visit(EnemyMakingDamage visitor)
    {
        return;
    }

    private void TakeDamage(float damage)
    {
        _health -= Mathf.Clamp(damage, 0, _health);

        OnTakingDamage.Invoke(damage);
        OnChangingHealth.Invoke(_health);

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }

    }

    private void Die()
    {
        OnDieing.Invoke();
        Destroy(gameObject);
    }
}

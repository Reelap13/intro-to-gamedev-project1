using Enemies;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTakingDamage : MonoBehaviour, IWeaponVisitor
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }
    [NonSerialized] public UnityEvent<Enemy> OnDieing = new UnityEvent<Enemy>();
    [NonSerialized] public UnityEvent<float> OnTakingDamage = new UnityEvent<float>();
    [NonSerialized] public UnityEvent<float> OnChangingHealth = new UnityEvent<float>();
    [SerializeField] private float _start_health = 10f;
    [SerializeField] private float _time_before_destroing = 3f;

    private float _health;

    private void Awake()
    {
        _health = _start_health;
    }

    public void Visit(EnemyMakingDamage visitor)
    {
        return;
    }
    public void Visit(CollisionScanProjectile visitor)
    {
        TakeDamage(visitor.Damage);
    }
    public void InstanceDie()
    {
        GetComponent<EnemyDrops>()?.BlockDropping();

        Die();
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
        OnDieing.Invoke(Enemy);

        Enemy.Animator.SetTrigger("Death");
        Enemy.Movement.Block();
        Enemy.Collider.enabled = false;
        Enemy.Agent.enabled = false;
        Debug.Log("Die");

        StartCoroutine(DestroyAnfterDieing());
    }
    private IEnumerator DestroyAnfterDieing()
    {
        yield return new WaitForSeconds(_time_before_destroing);
        Destroy(Enemy.gameObject);
    }

    public bool IsAlive { get { return _health > 0; } }
}

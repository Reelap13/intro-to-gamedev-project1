using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakingDamage : MonoBehaviour
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private BoxCollider _attacked_area;


    private bool _is_ready_to_attack;
    private void Awake()
    {
        _is_ready_to_attack = true;
    }

    public bool TryToAttack()
    {
        if (!_is_ready_to_attack) 
            return false;

        StartCoroutine(StartAttack());
        return true;
    }

    private IEnumerator StartAttack()
    {
        _is_ready_to_attack = false;
        Enemy.Animator.SetTrigger("Attack");

        yield return new WaitForSeconds(_cooldown);

        _is_ready_to_attack = true;
    }

    private void Attack()
    {
        Debug.Log("Attack");
        Collider[] targets = Physics.OverlapBox(_attacked_area.transform.position + _attacked_area.center, _attacked_area.size / 2);

        foreach (var target in targets)
            MakeDamage(target.gameObject);
    }

    private void MakeDamage(GameObject target)
    {
        Debug.Log(target.tag);
        if (target.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            visitor.Visit(this);
    }

    public float Damage { get { return _damage; } }
}

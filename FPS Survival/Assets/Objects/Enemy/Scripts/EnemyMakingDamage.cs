using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakingDamage : MonoBehaviour
{
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _cooldown = 1f;

    private bool _is_ready_to_attack;

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
        Attack();

        yield return new WaitForSeconds(_cooldown);

        _is_ready_to_attack = true;
    }

    private void Attack()
    {
        GameObject[] targets = { };

        // ...

        foreach (var target in targets)
            MakeDamage(target);
    }

    private void MakeDamage(GameObject target)
    {
        if (target.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            visitor.Visit(this);
    }
}

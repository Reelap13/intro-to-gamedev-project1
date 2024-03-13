using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }

    [SerializeField] private float _distance_to_attack = 3f;

    private void Update()
    {
        if (!Enemy.IsAlive)
            return;

        //Debug.Log($"{Enemy.Movement.DistanceToTarget} {_distance_to_attack}");
        if (Enemy.Movement.DistanceToTarget < _distance_to_attack)
            Enemy.MakingDamage.TryToAttack();
    }
}

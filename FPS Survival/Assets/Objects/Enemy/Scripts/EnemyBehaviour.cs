using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }

    [SerializeField] private float _distance_from_leader_to_start_chasing = 15f;
    [SerializeField] private float _distance_from_leader_to_finish_chasing = 23f;
    [SerializeField] private float _distance_to_attack = 3f;

    private void Update()
    {
        if (!Enemy.IsAlive)
            return;

        UpdateState();
        //Debug.Log($"{Enemy.Movement.DistanceToTarget} {_distance_to_attack}");
        if (Enemy.IsHasTarget && Enemy.Movement.DistanceToTarget < _distance_to_attack)
            Enemy.MakingDamage.TryToAttack();
    }

    private void UpdateState()
    {
        if (Enemy.Movement.State == EnemyMovementState.LEADER)
            return;
        else if (Enemy.Movement.State == EnemyMovementState.CHASE)
        {
            if (Vector3.Distance(LeaderSystem.Leader.Transform.position, Enemy.Transform.position) 
                < _distance_from_leader_to_finish_chasing)
            {
                Enemy.Movement.State = EnemyMovementState.ROAM;
            }
        }
        else if (Enemy.Movement.State == EnemyMovementState.ROAM)
        {
            if (Vector3.Distance(LeaderSystem.Leader.Transform.position, Enemy.Transform.position)
                < _distance_from_leader_to_start_chasing)
            {
                Enemy.Movement.State = EnemyMovementState.CHASE;
            }
        }
    }
}

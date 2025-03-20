using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemyMovement Movement { get; private set; }
    [field: SerializeField]
    public EnemyMakingDamage MakingDamage { get; private set; }
    [field: SerializeField]
    public EnemyTakingDamage TakingDamage { get; private set; }

    [field: SerializeField]
    public NavMeshAgent Agent { get; private set; }

    [field: SerializeField]
    public Animator Animator { get; private set; }

    [field: SerializeField]
    public Collider Collider { get; private set; }
    [field: SerializeField]
    public Transform Transform { get; private set; }
    [field: SerializeField]
    public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField]
    public Transform Target { get; private set; }
    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void SetWayPoint()
    {

    }
    public bool IsAlive => TakingDamage.IsAlive;
    public bool IsHasTarget => Target != null;
}

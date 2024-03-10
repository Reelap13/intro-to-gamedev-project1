using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: SerializeField]
    public EnemyMovementAI Movement { get; private set; }

    [field: SerializeField]
    public NavMeshAgent Agent { get; private set; }

    public Transform Target { get; private set; }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}

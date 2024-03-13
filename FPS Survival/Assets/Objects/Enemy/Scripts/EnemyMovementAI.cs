using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _angular_speed = 120f;
    [SerializeField] private float _accelertion = 8f;

    private NavMeshAgent _agent => Enemy.Agent;
    private Transform _target => Enemy.Target;

    public bool IsAccessMovement = true;

    private void Awake()
    {
        _agent.speed = _speed;
        _agent.angularSpeed = _angular_speed;
        _agent.acceleration = _accelertion;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        _agent.destination = _target.position;
    }
}

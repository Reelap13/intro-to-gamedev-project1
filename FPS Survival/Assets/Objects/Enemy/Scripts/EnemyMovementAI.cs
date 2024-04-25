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
    private Transform _transform => Enemy.Transform;
    private Transform _target => Enemy.Target;
    private Animator _animator => Enemy.Animator;

    public bool IsBlocking;

    private void Awake()
    {
        _agent.speed = _speed;
        _agent.angularSpeed = _angular_speed;
        _agent.acceleration = _accelertion;

        IsBlocking = false;
    }

    private void FixedUpdate()
    {
        //Debug.Log(IsAccessToMove() + " " + IsCanMove());
        if (!IsAccessToMove() || !IsCanMove())
            return;

        _agent.destination = _target.position;
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    protected bool IsCanMove()
    {
        return _target != null && _agent.isActiveAndEnabled && _agent.isOnNavMesh;
    }
    protected bool IsAccessToMove()
    {
        return !IsBlocking && Enemy.IsAlive;
    }

    public void LookToTarget()
    {
        if (_target == null) return;

        Vector3 _direction = (_target.position - _transform.position).normalized;
        _transform.rotation = Quaternion.LookRotation(_direction);
    }
    public void Block()
    {
        IsBlocking = false;
    }
    public void Unblock()
    {
        IsBlocking = true;
    }

    public float DistanceToTarget { get { return IsCanMove() ? _agent.remainingDistance : Mathf.Infinity; } }
}

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
    [SerializeField] private LayerMask obstaclesLayer;
    [SerializeField] private float distanceToPointThreshold = 0.1f;
    [SerializeField] private AStarAlgorithm aStar;

    private NavMeshAgent _agent => Enemy.Agent;
    private Transform _transform => Enemy.Transform;
    private Transform _target => Enemy.Target;
    private Animator _animator => Enemy.Animator;
    private List<Vector3Int> path;

    public bool IsBlocking;

    private void Awake()
    {
        _agent.speed = _speed;
        _agent.angularSpeed = _angular_speed;
        _agent.acceleration = _accelertion;

        IsBlocking = false;

        aStar = FindObjectOfType<AStarAlgorithm>();

    }

    private void FixedUpdate()
    {
        //Debug.Log(IsAccessToMove() + " " + IsCanMove());
        //if (!IsAccessToMove() || !IsCanMove())
        //    return;

        if(path == null)
        {
            path = aStar.AStarPathfinding(new((int)_transform.position.x, 0, (int)_transform.position.z),
    new((int)_target.position.x, 0, (int)_target.position.z));
            return;
        }

        if (path.Count > 1)
        {
            Vector3 targetVector = path[1] - _transform.position;
            targetVector.y = 0;
            Move(targetVector.normalized);
            float distance = Vector2.Distance(new(path[1].x, path[1].z), new(_transform.position.x, _transform.position.z));
            if (distance < distanceToPointThreshold)
            {
                path = aStar.AStarPathfinding(new((int)_transform.position.x, 0, (int)_transform.position.z),
        new((int)_target.position.x, 0, (int)_target.position.z));
            }
        }

        //_agent.destination = _target.position;
        _animator.SetFloat("Speed", _speed);
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

    public void Move(Vector3 targetVector)
    {
        _transform.rotation = Quaternion.LookRotation(new(targetVector.x, 0, targetVector.z));

        GetComponentInParent<Rigidbody>().velocity = _transform.forward * _speed;

    }

    public float DistanceToTarget { get { return IsCanMove() ? _agent.remainingDistance : Mathf.Infinity; } }
}

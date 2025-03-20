using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderMovement : EnemyMovementStateAbstr
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _time_to_update = 0.3f;
    [SerializeField] private float _skipping_point_distance = 0.5f;
    public Transform Target => EnemyMovement.Target;

    private List<Vector3> _way;
    private float _time = 0;

    private void Awake()
    {
        _time = _time_to_update;
    }

    public override Vector3 GetMovementDirection()
    {
        _time += Time.fixedDeltaTime;
        if (!IsActive || Target == null)
            return Vector3.zero;

        if (_time > _time_to_update)
        {
            UpdatePath();
            _time = 0;
        }
        RemoveClosedPoint();

        if (!IsCanMove)
            return Vector3.zero;

        Vector3 direction = _way.First() - Transform.position;
        direction.y = 0;
        return direction.normalized * _speed;
    }

    private void UpdatePath()
    {
        List<Vector3Int> way = AStarAlgorithm.Instance.AStarPathfinding(Transform.position, Target.position);
        if (way != null)
            _way = way.Select(v => (Vector3)v).ToList();
    }

    private void RemoveClosedPoint()
    {
        if (!IsCanMove) return;

        Vector3 point = _way.First();
        point.y = 0;

        Vector3 positin = Transform.position;
        positin.y = 0;

        if (Vector3.Distance(point, positin) <= _skipping_point_distance)
        {
            _way.RemoveAt(0);
            RemoveClosedPoint();
        }
    }

    private bool IsCanMove => Target != null && _way != null && _way.Count != 0;
}

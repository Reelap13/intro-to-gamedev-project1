using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : EnemyMovementStateAbstr
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _touching_distance = 1.0f;

    private WayPoint _previous_point;
    private WayPoint _aim_point;

    public void SetStartPoints(WayPoint start, WayPoint aim)
    {
        _previous_point = start;
        _aim_point = aim;
    }

    public override Vector3 GetMovementDirection()
    {
        if (!IsActive || !IsInitialize)
            return Vector3.zero;

        if (IsTouchPoint())
            UpdatePoints();

        return CalculateDirection().normalized * _speed;
    }

    private bool IsTouchPoint()
    {
        return CalculateDirection().magnitude <= _touching_distance;
    }

    private void UpdatePoints()
    {
        WayPoint temp = _previous_point;
        _previous_point = _aim_point;
        _aim_point = _aim_point.GetNeighbor(temp);
    }

    private Vector3 CalculateDirection()
    {
        Vector3 direction = _aim_point.transform.position - Transform.position;
        direction.y = 0;
        return direction;
    }

    private bool IsInitialize => _previous_point != null && _aim_point != null;
}

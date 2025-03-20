using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlockingMovement : EnemyMovementStateAbstr
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _cohesion_coef = 1.0f;
    [SerializeField] private float _alignment_coef = 1.0f;
    [SerializeField] private float _separation_coef = 1.0f;

    private FlockingSystem _system;
    public void Initialize(FlockingSystem system)
    {
        _system = system;
    }

    public override Vector3 GetMovementDirection()
    {
        if (!IsActive || !IsInitialize)
            return Vector3.zero;

        Vector2 cohesion = _system.Cohesion(EnemyMovement);
        Vector2 alignment = _system.Alignment(EnemyMovement);
        Vector2 separation = _system.Separation(EnemyMovement);

        Vector2 target = (cohesion * _cohesion_coef
            + alignment * _alignment_coef 
            + separation * _separation_coef).normalized;
        return new Vector3(target.x, 0, target.y) * _speed;
    }

    private bool IsInitialize => _system != null;
}

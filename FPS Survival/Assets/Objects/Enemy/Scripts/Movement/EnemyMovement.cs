using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }

    [SerializeField] private float _max_speed = 10f;
    [SerializeField] private float _angular_speed = 120f;

    [SerializeField] private WayPointMovement _way_point_movement;
    [SerializeField] private LeaderMovement _leader_movement;
    [SerializeField] private FlockingMovement _roam_flocking_movement;
    [SerializeField] private FlockingMovement _chase_flocking_movement;

    public Transform Transform => Enemy.Transform;
    public Transform Target => Enemy.Target;
    public Rigidbody Rigidbody => Enemy.Rigidbody;
    public Animator Animator => Enemy.Animator;

    public EnemyMovementState State { get; set; } = EnemyMovementState.UNKNOWN;
    public bool IsBlocking { get; private set; }

    private Vector3 _direction;

    private void Awake()
    {
        State = EnemyMovementState.ROAM;
    }

    private void FixedUpdate()
    {
        if (!IsAccessToMove())
            return;

        UpdateDirection();

        Rigidbody.velocity *= 0.9f;

        Rigidbody.MovePosition(Transform.position + _direction * Time.fixedDeltaTime);
        Transform.LookAt(transform.position + new Vector3(_direction.x, 0, _direction.z));
    }

    private void UpdateDirection()
    {
        Vector3 direction = _direction + GetNewDirection() * Time.fixedDeltaTime;
        if (direction.magnitude < Mathf.Epsilon)
            return;
        
        if (direction.magnitude > _max_speed)
            direction = direction.normalized * _max_speed;

        float angle = Vector3.Angle(_direction, direction);
        float angular_speed = _angular_speed * Time.fixedDeltaTime;
        if (angle <= angular_speed)
        {
            _direction = direction;
            return;
        }

        
        Quaternion rotation = Quaternion.RotateTowards(
            Quaternion.LookRotation(_direction),
            Quaternion.LookRotation(direction),
            angular_speed
            );

        _direction = rotation * Vector3.forward;
    }

    private Vector3 GetNewDirection()
    {
        switch (State)
        {
            case EnemyMovementState.ROAM:
                return _way_point_movement.GetMovementDirection()
                    + _roam_flocking_movement.GetMovementDirection();
            case EnemyMovementState.CHASE:
                return _chase_flocking_movement.GetMovementDirection();
            case EnemyMovementState.LEADER:
                return _leader_movement.GetMovementDirection();
        }

        return Vector3.zero;
    }

    public void SetStartWayPoint(WayPoint start, WayPoint aim)
    {
        State = EnemyMovementState.LEADER;
        //State = EnemyMovementState.ROAM;
        _way_point_movement.SetStartPoints(start, aim);
    }

    public void SetFlockingSystem(FlockingSystem system)
    {
        _roam_flocking_movement.Initialize(system);
    }

    protected bool IsAccessToMove()
    {
        return !IsBlocking && Enemy.IsAlive;
    }

    public void Block()
    {
        IsBlocking = false;
    }

    public void Unblock()
    {
        IsBlocking = true;
    }

    public float DistanceToTarget { get { return Enemy.IsHasTarget ? (Target.position - Transform.position).magnitude : Mathf.Infinity; } }
}

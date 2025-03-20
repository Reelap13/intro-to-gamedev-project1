using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class EnemyMovementStateAbstr : MonoBehaviour
{
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyMovementState State { get; private set; }

    public Transform Transform => EnemyMovement.Transform;

    public abstract Vector3 GetMovementDirection();
    public bool IsActive => EnemyMovement.State == State;
}

using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class FlockingSystem : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    public float neighborRadius = 5f;
    public float separationDistance = 2f;
    public float fovAngle = 90f;
    public int leaderMass = 100;

    private List<EnemyMovement> _enemies = new();

    private void Awake()
    {
        _spawner.OnEnemySpawning.AddListener(AddEnemy);
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy.Movement);
        enemy.Movement.SetFlockingSystem(this);
        enemy.TakingDamage.OnDieing.AddListener(RemoveEnemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy.Movement);
    }

    public Vector2 Cohesion(EnemyMovement enemy)
    {
        return FlockingAlgorithm.Cohesion(enemy, _enemies, neighborRadius, fovAngle, leaderMass);
    }
    public Vector2 Alignment(EnemyMovement enemy)
    {
        return FlockingAlgorithm.Alignment(enemy, _enemies, neighborRadius, fovAngle, leaderMass);
    }
    public Vector2 Separation(EnemyMovement enemy)
    {
        return FlockingAlgorithm.Separation(enemy, _enemies, separationDistance, fovAngle);
    }
}

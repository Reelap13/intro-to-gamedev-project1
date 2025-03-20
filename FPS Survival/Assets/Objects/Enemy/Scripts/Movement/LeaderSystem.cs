using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class LeaderSystem : MonoBehaviour
{
    [SerializeField] private LevelPlayerSpawner _player_spawner;
    [SerializeField] private EnemySpawner _enemies_spawner;
    [SerializeField] private float _recheck_time = 1.0f;

    private Player _player;
    private List<EnemyMovement> _enemies = new();
    public static EnemyMovement Leader { get; private set; }

    private void Awake()
    {
        _player_spawner.OnPlayerSpawning.AddListener(SetPlayer);
        _enemies_spawner.OnEnemySpawning.AddListener(AddEnemy);
        StartCoroutine(UpdateLeader());
    }

    private IEnumerator UpdateLeader()
    {
        while (true)
        {
            if (_player == null || _enemies.Count == 0)
                yield return null;

            EnemyMovement closest_enemy = null;
            float min_ditance = float.MaxValue;
            foreach (EnemyMovement enemy in _enemies)
            {
                float distance = Vector3.Distance(enemy.Transform.position, _player.transform.position);
                if (min_ditance > distance)
                {
                    min_ditance = distance;
                    closest_enemy = enemy;
                }
            }

            Leader.State = EnemyMovementState.CHASE;
            Leader = closest_enemy;
            Leader.State = EnemyMovementState.LEADER;
            yield return new WaitForSeconds(_recheck_time);
        }
    }
    
    private void SetPlayer(Player player)
    {
        _player = player;
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy.Movement);
        enemy.TakingDamage.OnDieing.AddListener(RemoveEnemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy.Movement);
    }

}

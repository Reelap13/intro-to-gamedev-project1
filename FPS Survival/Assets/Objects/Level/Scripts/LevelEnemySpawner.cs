using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{
    [field: SerializeField]
    public LevelController LevelController { get; private set; }
    [SerializeField] private GameObject _enemy_pref;
    [SerializeField] private Transform _enemy_directory;
    [SerializeField] private int _const_spawn_number = 5;
    [SerializeField] private int _dynamic_spawn_number = 1;
    [SerializeField] private float _distance_from_player_to_spawn = 10;

    private Transform _player;
    private int _wave_number;
    private int _number_alive_enemies;
    private void Awake()
    {
        _wave_number = 0;
        _number_alive_enemies = 0;
    }

    public void StartSpawningEnemy()
    {
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        ++_wave_number;
        _number_alive_enemies = _const_spawn_number + _dynamic_spawn_number * _wave_number;

        List<Vector3> spawn_positions = GetSpawnPoints();
        for (int i = 0; i < _number_alive_enemies; ++i)
            SpawnEnemy(spawn_positions[Random.Range(0, spawn_positions.Count)]);
    }

    private void SpawnEnemy(Vector3 spawn_point)
    {
        GameObject enemy_obj = Instantiate(_enemy_pref) as GameObject;
        enemy_obj.transform.parent = _enemy_directory;
        enemy_obj.transform.position = spawn_point;

        Enemy enemy = enemy_obj.GetComponent<Enemy>();
        enemy.SetTarget(_player);
        enemy.TakingDamage.OnDieing.AddListener(MarkDeathOfEnemy);
    }
    private List<Vector3> GetSpawnPoints()
    {
        List<Vector3> spawn_positions = new List<Vector3>();
        foreach(var tile in LevelController.LevelGenerator.Level.Values)
        {
            if ((tile.transform.position - _player.transform.position).magnitude >= _distance_from_player_to_spawn)
                spawn_positions.Add(tile.transform.position);
        }
        return spawn_positions;
    }

    private void MarkDeathOfEnemy()
    {
        --_number_alive_enemies;
        if (_number_alive_enemies <= 0)
            SpawnNextWave();
    }
    public void SetPlayer(Transform player)
    {
        _player = player;
    }
}

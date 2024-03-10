using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _enemy_pref;
    [SerializeField] private Transform _spawn_point;
    [SerializeField] private Transform _enemy_directory;

    public void StartSpawningEnemy()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject enemy_obj = Instantiate(_enemy_pref) as GameObject;
        enemy_obj.transform.parent = _enemy_directory;
        enemy_obj.transform.position = _spawn_point.position;

        Enemy enemy = enemy_obj.GetComponent<Enemy>();
        enemy.SetTarget(_player);
    }
}

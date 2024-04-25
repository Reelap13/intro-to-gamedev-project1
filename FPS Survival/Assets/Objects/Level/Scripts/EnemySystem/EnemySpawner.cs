using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [field: SerializeField]
        public EnemySystem EnemySystem { get; private set; }

        [SerializeField] private EnemyPrefab _enemy_pref;
        [SerializeField] private int _const_spawn_number = 5;
        [SerializeField] private float _distance_from_player_to_spawn = 15;

        [SerializeField] private float _spawning_rate = 10f;
        [SerializeField] private Vector2Int _spawning_enemies_number = new Vector2Int(3, 4);

        private HashSet<Enemy> _enemies = new HashSet<Enemy>();
        private float time = 0;

        private void Awake()
        { 

        }

        private void Update()
        {
            if (!EnemySystem.IsSpawning)
                return;

            time += Time.deltaTime;
            if (time >= _spawning_rate)
            {
                time -= _spawning_rate;
                SpawnNextWave();
            }
        }

        private void SpawnNextWave()
        {
            int enemies_number = Random.Range(_spawning_enemies_number.x, _spawning_enemies_number.y + 1);
            for (int i = 0; i < enemies_number; ++i)
                SpawnEnemy(_enemy_pref.GetEnemyPref());
            EnemySystem.LevelController.LevelGenerator.Surface.enabled = false;
            EnemySystem.LevelController.LevelGenerator.Surface.enabled = true;
        }

        private void SpawnEnemy(GameObject prefab)
        {
            GameObject enemy_obj = Instantiate(_enemy_pref.GetEnemyPref()) as GameObject;
            enemy_obj.transform.parent = EnemySystem.EnemyDirectory;
            enemy_obj.transform.position = GetPositionToSpawnEnemy();

            Enemy enemy = enemy_obj.GetComponent<Enemy>();
            enemy.SetTarget(EnemySystem.Target);
            enemy.TakingDamage.OnDieing.AddListener(UnregisterEnemy);

            RegisterEnemy(enemy);
        }

        private Vector3 GetPositionToSpawnEnemy()
        {
            Vector3 direction = new Vector3(_distance_from_player_to_spawn, 0, 0);
            float rad = Random.Range(0, 360) * Mathf.Deg2Rad;

            float cos_angle = Mathf.Cos(rad);
            float sin_angle = Mathf.Sin(rad);

            float rotatedX = direction.x * cos_angle - direction.z * sin_angle;
            float rotatedZ = direction.x * sin_angle + direction.z * cos_angle;

            Vector3 position = EnemySystem.LevelController.LevelGenerator.BoardPosition(new Vector3(rotatedX, 0, rotatedZ) + EnemySystem.Target.position, 4);

            return EnemySystem.LevelController.LevelGenerator.GetFreePoint(position);
        }

        private void RegisterEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }
        private void UnregisterEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}
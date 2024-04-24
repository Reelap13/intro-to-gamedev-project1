using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [field: SerializeField]
    public LevelGenerator.LevelGenerator LevelGenerator { get; private set; }
    [field: SerializeField]
    public LevelPlayerSpawner PlayerSpawner { get; private set; }
    [field: SerializeField]
    public LevelEnemySpawner EnemySpawner { get; private set; }

    private void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        LevelGenerator.GenerateLevel();

        GameObject player = PlayerSpawner.CreatePlayer();

        //EnemySpawner.SetPlayer(player.transform);
        //EnemySpawner.StartSpawningEnemy();
    }
}

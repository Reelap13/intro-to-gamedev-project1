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
    public Enemies.EnemySystem EnemySystem { get; private set; }
    [field: SerializeField]
    public GameController.Controller GameController { get; private set; }

    private void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        LevelGenerator.GenerateLevel();

        Player player = PlayerSpawner.CreatePlayer();

        EnemySystem.SetPreset(player.transform);

        GameController.Initialize(player);
        GameController.StartGame();
    }
}

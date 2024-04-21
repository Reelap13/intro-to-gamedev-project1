using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    [SerializeField] private Enemy _easy_enemy;
    [SerializeField] private Enemy _normal_enemy;
    [SerializeField] private Enemy _hard_enemy;

    public GameObject GetEnemyPref()
    {
        switch (Setting.Difficulty.DifficultyType)
        {
            case Settings.DifficultyType.Easy: return _easy_enemy.gameObject;
            case Settings.DifficultyType.Normal: return _normal_enemy.gameObject;
            case Settings.DifficultyType.Hard: return _hard_enemy.gameObject;
            default: return null;
        }
    }
}

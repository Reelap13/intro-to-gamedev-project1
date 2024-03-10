using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player_pref;
    [SerializeField] private Transform _spawn_point;
    [SerializeField] private Transform _player_directory;

    private GameObject _player;

    private void Awake()
    {
        CreatePlayer();
    }

    public GameObject CreatePlayer()
    {
        _player = SpawnPlayer(_spawn_point.position);
        return _player;
    }
    private void RespawnPlayer()
    {
        _player = SpawnPlayer(_player.transform.position);
    }
    private GameObject SpawnPlayer(Vector3 spawn_point)
    {
        GameObject player_obj = Instantiate(_player_pref) as GameObject;
        player_obj.transform.parent = _player_directory;
        player_obj.transform.position = spawn_point;

        return player_obj;
    }
}

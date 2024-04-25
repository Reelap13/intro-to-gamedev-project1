using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelPlayerSpawner : MonoBehaviour
{
    [field: SerializeField]
    public LevelController LevelController { get; private set; }
    [SerializeField] private GameObject _player_pref;
    [SerializeField] private Vector3 _spawning_offset = Vector3.up * 5;
    [SerializeField] private Transform _player_directory;
    [SerializeField] private Vector3 _player_camera_position = Vector3.up * 0.6f;

    private Player _player;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

    public GameObject CreatePlayer()
    {
        _player = SpawnPlayer(LevelController.LevelGenerator.GetCenter() + _spawning_offset);

        return _player.gameObject;
    }
    private void RespawnPlayer()
    {
        _player = SpawnPlayer(_player.transform.position);
    }
    private Player SpawnPlayer(Vector3 spawn_point)
    {
        GameObject player_obj = Instantiate(_player_pref) as GameObject;
        player_obj.transform.parent = _player_directory;
        player_obj.transform.position = spawn_point;
        Player player = player_obj.GetComponent<Player>();
        SetCamera(player);

        return player;
    }

    private void SetCamera(Player player)
    {
        _camera.transform.parent = player.transform;
        _camera.transform.localPosition = _player_camera_position;
        _camera.GetComponent<CameraLook>().SetPreset(player);
    }
}

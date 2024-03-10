using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private TilesTemplatesSet _tiles_set;
    [SerializeField] private Vector2 _level_size = Vector2.one;
    [SerializeField] private Vector3 _tile_size = Vector3.one;

    private Dictionary<Pair<int, int>, GameObject> _level;
    private void Awake()
    {
        GenerateLevel();
    }
    public void GenerateLevel()
    {
        _level = new Dictionary<Pair<int, int>, GameObject>();
        for (int i = 0; i < _level_size.x; ++i)
        {
            for (int j = 0; j < _level_size.y; ++j)
            {
                GameObject tile_pref = _tiles_set.GetRandomTilePrefab();
                GameObject tile = CreateTile(tile_pref, i, j);
                _level.Add(new Pair<int, int>(i, j), tile);
            }
        }
    }

    private GameObject CreateTile(GameObject tile_pref, int i, int j)
    {
        GameObject tile = Instantiate(tile_pref) as GameObject;
        tile.transform.parent = transform;
        tile.transform.position = new Vector3(_tile_size.x * i, 0, _tile_size.z * j);

        return tile;
    }
}

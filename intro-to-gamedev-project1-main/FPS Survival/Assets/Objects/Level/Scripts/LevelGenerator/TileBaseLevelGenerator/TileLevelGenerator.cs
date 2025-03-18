using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using LevelGenerator.TileGeneration;


namespace LevelGenerator.TileGeneration
{
    public class TileLevelGenerator : LevelGenerator
    {
        [SerializeField] private NavMeshSurface _surface;
        [SerializeField] private Transform _level_directory;
        [SerializeField] private TilesTemplatesSet _tiles_set;
        [SerializeField] private Vector2 _level_size = Vector2.one;
        [SerializeField] private Vector3 _tile_size = Vector3.one;

        private Dictionary<Pair<int, int>, GameObject> _level;
        private Vector3 _center;

        public override void GenerateLevel()
        {
            int center_x = Convert.ToInt32(_level_size.x / 2 - 0.5);
            int center_y = Convert.ToInt32(_level_size.y / 2 - 0.5);
            _center = new Vector3(center_x, 0, center_y);

            _level = new Dictionary<Pair<int, int>, GameObject>();
            for (int i = 0; i < _level_size.x; ++i)
            {
                for (int j = 0; j < _level_size.y; ++j)
                {
                    if (i == center_x && j == center_y)
                        continue;
                    GameObject tile_pref = _tiles_set.GetRandomTilePrefab();
                    GameObject tile = CreateTile(tile_pref, i, j);
                    _level.Add(new Pair<int, int>(i, j), tile);
                }
            }

            _level.Add(new Pair<int, int>(center_x, center_y), CreateTile(_tiles_set.DefaultTile, center_x, center_y));
            _surface.BuildNavMesh();
        }

        private GameObject CreateTile(GameObject tile_pref, int i, int j)
        {
            GameObject tile = Instantiate(tile_pref) as GameObject;
            tile.transform.parent = _level_directory;
            tile.transform.position = new Vector3(_tile_size.x * i, 0, _tile_size.z * j);
            tile.name = $"Tile {i} {j}";

            return tile;
        }
        public override Vector3 GetCenter()
        {
            return new Vector3(_center.x * _tile_size.x, 0, _center.z * _tile_size.z);
        }
        public Dictionary<Pair<int, int>, GameObject> Level { get { return _level; } }
    }
}

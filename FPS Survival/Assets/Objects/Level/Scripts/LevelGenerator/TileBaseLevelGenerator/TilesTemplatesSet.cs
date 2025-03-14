using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.TileGeneration
{
    public class TilesTemplatesSet : MonoBehaviour
    {
        [SerializeField] private GameObject _start_tile_pref;
        [SerializeField] private TemplateTile[] _tiles;

        private float _frequencies_sum;

        private void Awake()
        {
            CalculateFrequenciesSum();
        }

        public GameObject GetRandomTilePrefab()
        {
            float rand = Random.Range(0.0f, _frequencies_sum);
            return GetTile(rand).TilePrefab;
        }
        private void CalculateFrequenciesSum()
        {
            _frequencies_sum = 0;
            foreach (var tile in _tiles)
                _frequencies_sum += tile.Frequency;
        }
        private TemplateTile GetTile(float value)
        {
            foreach (var tile in _tiles)
            {
                value -= tile.Frequency;
                if (value < 0.0f)
                    return tile;
            }
            return _tiles[0];
        }

        public GameObject DefaultTile { get { return _start_tile_pref; } }
    }
}

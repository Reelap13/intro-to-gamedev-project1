using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public class TerrainHeights : LevelGeneratorElement
    { 
        [SerializeField] private float _scale = 20f;

        private float[,] _heights;

        public float[,] GenerateHeights()
        {
            float[,] heights = new float[_width, _height];
            Vector2 offset = CalculateOffset();

            for (int x = 0; x < _width; ++x)
            {
                for (int y = 0; y < _height; ++y)
                {
                    heights[x, y] = Mathf.PerlinNoise((x + offset.x) / _scale, (y + offset.y) / _scale);
                }
            }

            _heights = heights;
            return heights;
        }

        public float[,] Heights { get { return _heights; } }
    }
}

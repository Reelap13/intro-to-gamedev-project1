using LevelGenerator.PerlinNoiseGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public class Roads : LevelGeneratorElement
    {
        [SerializeField] private Texture2D _texture;
        [SerializeField] private float _scale = 20f;
        [SerializeField] private float _interval = 0.05f;

        private float _deffault_value = 0.5f;

        public FloatArray2D RoadMap { get; private set; }
        public FloatArray2D GenerateRoadMap()
        {
            RoadMap = new FloatArray2D(_width, _height);
            Vector2 offset = CalculateOffset();

            for (int x = 0; x < _width; ++x)
            {
                for (int y = 0; y < _height; ++y)
                {
                    if (Generator.BlockedMap[x, y] == 1)
                        continue;

                    float perlinValue = Mathf.PerlinNoise((x + offset.x) / _scale, (y + offset.y) / _scale);
                    RoadMap[x, y] = (_deffault_value - _interval < perlinValue && perlinValue < _deffault_value + _interval) ? 1 : 0;
                }
            }

            return RoadMap;
        }

        public Texture2D Texture { get { return _texture; } }
    }
}
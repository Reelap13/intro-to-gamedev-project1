using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public abstract class LevelGeneratorElement : MonoBehaviour
    {
        [field: SerializeField]
        public PerlinNoiseLevelGenerator Generator { get; private set; }
        [SerializeField] private Vector2 _offset_coefficient = new Vector2(1000, 1000);

        protected int _width => Generator.Width;
        protected int _height => Generator.Height;
        protected int _seed => Generator.Seed;

        protected Vector2 CalculateOffset()
        {
            return new Vector2(
                _seed % _offset_coefficient.x,
                _seed % _offset_coefficient.y);
        }
    }
}
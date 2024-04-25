using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesGeneratorElement : MonoBehaviour
    {
        [field: SerializeField]
        public CitiesGenerator Generator { get; private set; }
        [SerializeField] protected Vector2 _offset_coefficient = new Vector2(1000, 1000);

        protected int _width => Generator.Generator.Width;
        protected int _height => Generator.Generator.Height;
        protected int _seed => Generator.Generator.Seed;

        protected Vector2 CalculateOffset()
        {
            return new Vector2(
                _seed % _offset_coefficient.x,
                _seed % _offset_coefficient.y);
        }
    }
}
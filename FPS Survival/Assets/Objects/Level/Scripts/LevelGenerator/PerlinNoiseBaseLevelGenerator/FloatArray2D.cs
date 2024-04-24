using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public class FloatArray2D
    {
        private float[,] _data;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public FloatArray2D(int width, int height)
        {
            Width = width;
            Height = height;
            _data = new float[width, height];
        }

        public float this[int x, int y]
        {
            get { return _data[x, y]; }
            set { _data[x, y] = value; }
        }

        public bool IsCellExists(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public List<Vector2Int> GetNeighborCells(int x, int y)
        {
            return GetCellsInRange(x, y, 1);
        }

        public List<Vector2Int> GetCellsInRange(int x, int y, int range)
        {
            List<Vector2Int> cellsInRange = new List<Vector2Int>();

            for (int dx = -range; dx <= range; dx++)
            {
                for (int dy = -range; dy <= range; dy++)
                {
                    int neighborX = x + dx;
                    int neighborY = y + dy;

                    if (IsCellExists(neighborX, neighborY) && Math.Abs(dx) + Math.Abs(dy) <= range)
                    {
                        cellsInRange.Add(new Vector2Int(neighborX, neighborY));
                    }
                }
            }

            return cellsInRange;
        }

        public FloatArray2D GenerateGradientBiomMap(FloatArray2D blockedMap, int gradientRange, bool reverse = false)
        {
            FloatArray2D gradientBiomMap = new FloatArray2D(Width, Height);
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    if (blockedMap[x, y] != 1)
                    {
                        gradientBiomMap[x, y] = reverse ? 1 : 0;
                        continue;
                    }

                    gradientBiomMap[x, y] = reverse ? 0 : 1;
                    List<Vector2Int> nearestPoints = GetCellsInRange(x, y, gradientRange - 1);
                    foreach (Vector2Int point in nearestPoints)
                    {
                        float distance = Mathf.Abs(point.x - x) + Mathf.Abs(point.y - y);
                        gradientBiomMap[point.x, point.y] = reverse ? Mathf.Clamp(distance / gradientRange, 0, gradientBiomMap[point.x, point.y]) :
                                                                      Mathf.Clamp(distance / gradientRange, gradientBiomMap[point.x, point.y], 1);
                    }
                }
            }

            return gradientBiomMap;
        }

        public FloatArray2D Clone()
        {
            FloatArray2D clone = new FloatArray2D(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    clone[x, y] = _data[x, y];
                }
            }
            return clone;
        }
        public List<Vector2Int> GetCellsByValueInRange(float value, int range)
        {
            List<Vector2Int> cellsWithValueInRange = new List<Vector2Int>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Math.Abs(_data[x, y] - value) <= range)
                    {
                        cellsWithValueInRange.Add(new Vector2Int(x, y));
                    }
                }
            }

            return cellsWithValueInRange;
        }
    }
}
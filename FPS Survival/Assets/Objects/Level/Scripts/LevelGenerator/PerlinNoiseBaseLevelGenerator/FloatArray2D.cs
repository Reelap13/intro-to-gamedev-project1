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

                    if (IsCellExists(neighborX, neighborY) && (Math.Abs(dx) + Math.Abs(dy)) <= range)
                    {
                        cellsInRange.Add(new Vector2Int(neighborX, neighborY));
                    }
                }
            }

            return cellsInRange;
        }

        public static FloatArray2D GenerateGradientBiomMap(FloatArray2D blockedMap, int gradientRange, bool reverse = false)
        {
            FloatArray2D gradientBiomMap = new FloatArray2D(blockedMap.Width, blockedMap.Height);
            for (int x = 0; x < blockedMap.Width; ++x)
                for (int y = 0; y < blockedMap.Height; ++y)
                    if (blockedMap[x, y] == 1)
                        gradientBiomMap[x, y] = reverse ? 0 : 1;


            for (int x = 0; x < blockedMap.Width; ++x)
            {
                for (int y = 0; y < blockedMap.Height; ++y)
                {
                    if (blockedMap[x, y] != 1)
                    {
                        continue;
                    }

                    gradientBiomMap[x, y] = reverse ? 0 : 1;
                    List<Vector2Int> nearestPoints = gradientBiomMap.GetCellsInRange(x, y, gradientRange - 1);
                    foreach (Vector2Int point in nearestPoints)
                    {
                        float distance = Mathf.Abs(point.x - x) + Mathf.Abs(point.y - y);
                        gradientBiomMap[point.x, point.y] = reverse ? Mathf.Clamp(distance / gradientRange, 0, gradientBiomMap[point.x, point.y]) :
                                                                      Mathf.Clamp(1 - distance / gradientRange, gradientBiomMap[point.x, point.y], 1);
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
        public List<Vector2Int> GetCellsByValueInRange(float min, float max)
        {
            List<Vector2Int> cellsWithValueInRange = new List<Vector2Int>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (min <= _data[x, y] && _data[x, y] <= max)
                    {
                        cellsWithValueInRange.Add(new Vector2Int(x, y));
                    }
                }
            }

            return cellsWithValueInRange;
        }
        public Vector2 GetVectorForNeighbors(int centerX, int centerY, int radius)
        {
            Vector2 vector_sum = Vector2.zero;
            List<Vector2Int> neighbors = GetCellsInRange(centerX, centerY, radius);

            foreach (Vector2Int neighbor in neighbors)
            {
                int dx = neighbor.x - centerX;
                int dy = neighbor.y - centerY;
                float value = _data[neighbor.x, neighbor.y];
                float distance = Mathf.Sqrt(dx * dx + dy * dy);
                if (distance > 0 && distance <= radius)
                {
                    float factor = value / distance;
                    Vector2 offset = new Vector2(dx, dy).normalized * factor; 
                    vector_sum += offset;
                    Debug.Log($"{neighbor} {value} {distance} {factor} {offset} {vector_sum}");
                }
            }

            return vector_sum;
        }
        public Vector2 GetClosestHighestValueCoordinates(int x, int y, int radius)
        {
            Vector2 closestCoordinates = new Vector2(float.NaN, float.NaN);
            float highestValue = float.MinValue;
            int count = 0;
            Vector2 sum = Vector2.zero;

            List<Vector2Int> neighbors = GetCellsInRange(x, y, radius);

            foreach (Vector2Int neighbor in neighbors)
            {
                if (IsCellExists(neighbor.x, neighbor.y))
                {
                    float value = _data[neighbor.x, neighbor.y];
                    if (value > highestValue)
                    {
                        highestValue = value;
                        closestCoordinates = new Vector2(neighbor.x, neighbor.y);
                        count = 1;
                        sum = closestCoordinates;
                    }
                    else if (value == highestValue)
                    {
                        closestCoordinates += new Vector2(neighbor.x, neighbor.y);
                        count++;
                    }
                }
            }

            if (!float.IsNaN(closestCoordinates.x) && !float.IsNaN(closestCoordinates.y))
            {
                return closestCoordinates / count;
            }
            else
            {
                return Vector2.zero;
            }
        }
        public void SetValueInRange(int x, int y, int radius, float value)
        {
            List<Vector2Int> cellsInRange = GetCellsInRange(x, y, radius);

            foreach (Vector2Int cell in cellsInRange)
            {
                if (IsCellExists(cell.x, cell.y))
                {
                    _data[cell.x, cell.y] = value;
                }
            }
        }
        public void ReverseValues()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _data[x, y] = 1 - _data[x, y];
                }
            }
        }
    }
}
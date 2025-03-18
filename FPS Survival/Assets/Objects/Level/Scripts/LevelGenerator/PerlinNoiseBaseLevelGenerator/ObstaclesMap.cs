using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public class ObstaclesMap : LevelGeneratorElement
    {
        [SerializeField] private LayerMask obstaclesLayer;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool[,] _obstaclesMap;

        public bool[,] GenerateObstaclesMap(float[,] heights)
        {
            bool[,] obstaclesMap = new bool[_width, _height];
            Texture2D texture = new Texture2D(obstaclesMap.GetLength(0), obstaclesMap.GetLength(1));
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Vector3 position = new(i, heights[i, j] * 20, j);
                    if(Physics.OverlapSphere(position, 0.5f, obstaclesLayer).Length > 1)
                    {
                        obstaclesMap[i, j] = true;
                        Debug.Log(i + " " + j);
                    }
                    Color pixelColor = obstaclesMap[i, j] ? Color.white : Color.black;
                    texture.SetPixel(i, j, pixelColor);
                }
            }

            texture.Apply();

            // Create a Sprite from the texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Assign the Sprite to the SpriteRenderer
            spriteRenderer.sprite = sprite;

            _obstaclesMap = obstaclesMap;
            return obstaclesMap;
        }

        public bool[,] Obstacles { get { return _obstaclesMap; } }
    }
}


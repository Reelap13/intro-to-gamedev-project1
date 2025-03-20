using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public class ObstaclesMap : LevelGeneratorElement
    {
        [SerializeField] private LayerMask obstaclesLayer;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float scanHeight = 2.0f;
        [SerializeField] private float scanWidth = 0.5f;

        private bool[,] _obstaclesMap;

        public void GenerateObstaclesMap(Terrain terrain)
        {
            StartCoroutine(asf(terrain));
        }

        private IEnumerator asf(Terrain terrain)
        {
            yield return null;
            GenerateObstaclesMap1(terrain);
        }

        public bool[,] GenerateObstaclesMap1(Terrain terrain)
        {
            bool[,] obstaclesMap = new bool[_width, _height];
            Texture2D texture = new Texture2D(obstaclesMap.GetLength(0), obstaclesMap.GetLength(1));
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Vector3 position = new(i, 0, j);
                    position.y = terrain.SampleHeight(position);

                    Vector3 boxSize = new Vector3(scanWidth, scanHeight, scanWidth);
                    if (Physics.OverlapBox(position, boxSize / 2, Quaternion.identity, obstaclesLayer).Length > 0)
                    {
                        obstaclesMap[i, j] = true;
                        Debug.Log(i + " " + j);
                    }
                    Color pixelColor = obstaclesMap[i, j] ? Color.white : Color.black;
                    texture.SetPixel(i, j, pixelColor);
                }
            }

            texture.Apply();

            //Save texture
            byte[] bytes = texture.EncodeToPNG();
            string path = Application.dataPath + "/obstacle_map.png";
            File.WriteAllBytes(path, bytes);

            // Create a Sprite from the texture
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Assign the Sprite to the SpriteRenderer
            //spriteRenderer.sprite = sprite;

            _obstaclesMap = obstaclesMap;
            return obstaclesMap;
        }

        public bool[,] Obstacles { get { return _obstaclesMap; } }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Environment
{
    public class BiomFiller : EnvironmentGeneratorElement
    {
        [SerializeField] private BiomObject[] _objects;
        private float size = 1f;
        public void FillBioms()
        {

            Random.State previous_state = Random.state;
            Vector2 offset = CalculateOffset();
            Random.InitState(_seed % (int)offset.magnitude);

            FloatArray2D biom_gradient = Generator.Biom.GradientBiomMap;
            int count = 0;
            for (int x = 0; x < _width; ++x)
            {
                for (int y = 0; y < _height; ++y)
                {
                    if (biom_gradient[x, y] != 1) count++;
                    ProcessPoint(x, y, biom_gradient[x, y]);
                }
            }
            Debug.Log(count + " " + (biom_gradient.Width * biom_gradient.Height - count));
            Random.state = previous_state;
        }

        private void ProcessPoint(int x, int y, float value)
        {
            float prob = Random.Range(0.0f, 1.0f) * value;

            foreach (BiomObject obj in _objects)
            {
                if (obj.Prefabs.Length == 0)
                    continue;

                if (prob > obj.MinProbability)
                {
                    int number = (int)Mathf.Round(Random.Range(0, obj.Frequency) * value);
                    for (int i = 0; i < number; ++i)
                        CreateObject(x, y, obj.Prefabs[Random.Range(0, obj.Prefabs.Length)]).name = $"{value} {prob} {number} {x} {y}";
                }
            }
        }

        private GameObject CreateObject(int x, int y, GameObject prefab)
        {
            GameObject obj = Instantiate(prefab) as GameObject;
            obj.transform.parent = Generator.Generator.LevelDirectory;
            Vector3 position = new Vector3(
                Random.Range(y - size / 2, y + size / 2),
                0,
                Random.Range(x - size / 2, x + size / 2));
            obj.transform.position = new Vector3(position.x, Generator.Generator.Terrain.SampleHeight(position), position.z);
            obj.transform.rotation = Quaternion.Euler(obj.transform.eulerAngles + Vector3.up * Random.Range(0, 360));
            return obj;
        }
    }
}
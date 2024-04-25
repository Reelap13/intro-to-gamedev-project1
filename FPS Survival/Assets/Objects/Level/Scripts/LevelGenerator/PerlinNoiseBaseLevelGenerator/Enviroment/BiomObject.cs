using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Environment
{
    public class BiomObject : MonoBehaviour
    {
        public GameObject[] Prefabs;
        public float MinProbability = 0.5f;
        public float Frequency = 1f;
    }
}
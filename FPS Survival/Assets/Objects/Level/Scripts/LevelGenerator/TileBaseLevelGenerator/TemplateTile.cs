using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.TileGeneration
{
    public class TemplateTile : MonoBehaviour
    {

        [field: SerializeField]
        public GameObject TilePrefab { get; private set; }
        [field: SerializeField]
        public float Frequency { get; private set; } = 1f;
    }
}
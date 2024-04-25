using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator
{
    public abstract class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField]
        public Transform LevelDirectory { get; private set; }
        public abstract void GenerateLevel();
        public abstract Vector3 GetCenter();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator
{
    public abstract class LevelGenerator : MonoBehaviour
    {
        public abstract void GenerateLevel();
        public abstract Vector3 GetCenter();
    }
}
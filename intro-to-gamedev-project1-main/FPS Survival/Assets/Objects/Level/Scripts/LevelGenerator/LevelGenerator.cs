using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace LevelGenerator
{
    public abstract class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField]
        public Transform LevelDirectory { get; private set; }
        [field: SerializeField]
        public NavMeshSurface Surface { get; private set; }
        public abstract void GenerateLevel();
        public abstract Vector3 GetCenter();
        public virtual Vector3 GetFreePoint(Vector3 position) { return position; }
        public virtual Vector3 BoardPosition(Vector3 position, float bounds) {  return position; }
    }
}
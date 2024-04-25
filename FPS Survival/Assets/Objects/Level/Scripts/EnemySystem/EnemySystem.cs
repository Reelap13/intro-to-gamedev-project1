using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySystem : MonoBehaviour
    {
        [field: SerializeField]
        public LevelController LevelController { get; private set; }
        [field: SerializeField] public Transform EnemyDirectory { get; private set; }
        
        public Transform Target { get; private set; }
        public bool IsSpawning { get; private set; } = false;


        public void SetPreset(Transform target) { Target = target; }

        public void StartSpawningEnemies()
        {
            IsSpawning = true;
        }
        private void StopSpawningEnemies()
        {
            IsSpawning = false;
        }
    }
}
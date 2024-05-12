using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyDrops : MonoBehaviour
    {
        [field: SerializeField]
        public Enemy Enemy { get; private set; }
        [SerializeField] private GameObject[] _droping_items;
        private float[] probabilities;

        private void Awake()
        {
            Enemy.TakingDamage.OnDieing.AddListener((Enemy enemy) => DropItem());
        }

        private void DropItem()
        {
            if(Random.value < 0.3)
            {
                // Convert the values to probabilities
                float sum = 0f;
                float[] values = new float[_droping_items.Length];
                for (int i = 0; i < _droping_items.Length; i++)
                {
                    float value = _droping_items[i].GetComponent<ItemData>().amount;
                    values.SetValue(value, i);
                    sum += value;
                }

                probabilities = new float[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    probabilities[i] = values[i] / sum;
                }
                GameObject item_pref = _droping_items[ChooseItem()];
                GameObject item = CreateDropedItem(item_pref);
            }
        }

        private GameObject CreateDropedItem(GameObject item_pref)
        {
            GameObject item = Instantiate(item_pref) as GameObject;
            item.transform.position = Enemy.Transform.position;

            return item;
        }

        private int ChooseItem()
        {
            // Generate a random number between 0 and 1
            float randomNumber = Random.value;

            // Find the first probability that is greater than the random number
            int chosenIndex = -1;
            float currentProbability = 0f;
            for (int i = 0; i < probabilities.Length; i++)
            {
                currentProbability += probabilities[i];
                if (randomNumber <= currentProbability)
                {
                    chosenIndex = i;
                    break;
                }
            }

            return chosenIndex;
        }
    }
}
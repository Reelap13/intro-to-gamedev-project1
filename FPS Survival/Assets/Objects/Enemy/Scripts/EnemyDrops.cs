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

        private bool _is_access_to_drop = true;

        private void Awake()
        {
            Enemy.TakingDamage.OnDieing.AddListener((Enemy enemy) => DropItem());
        }

        private void DropItem()
        {
            if (!_is_access_to_drop)
                return;

            GameObject item_pref = _droping_items[Random.Range(0, _droping_items.Length)];
            GameObject item = CreateDropedItem(item_pref);
        }

        private GameObject CreateDropedItem(GameObject item_pref)
        {
            GameObject item = Instantiate(item_pref) as GameObject;
            item.transform.position = Enemy.Transform.position;

            return item;
        }

        public void BlockDropping()
        {
            _is_access_to_drop = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }
    [SerializeField] private GameObject[] _droping_items;

    private void Awake()
    {
        Enemy.TakingDamage.OnDieing.AddListener(DropItem);
    }

    private void DropItem()
    {
        GameObject item_pref = _droping_items[Random.Range(0, _droping_items.Length)];
        GameObject item = CreateDropedItem(item_pref);
    }

    private GameObject CreateDropedItem(GameObject item_pref)
    {
        GameObject item = Instantiate(item_pref) as GameObject;
        item.transform.position = Enemy.Transform.position;

        return item;
    }
}

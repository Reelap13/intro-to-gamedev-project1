using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Inventory;

public class PlayerInventoryController : MonoBehaviour
{
    private Inventory.Inventory _inventory;
    [SerializeField] private float pickupDistance;

    [NonSerialized] public UnityEvent<ItemAmount> OnAddItem = new UnityEvent<ItemAmount>();
    [NonSerialized] public UnityEvent<ItemAmount> OnRemoveItem = new UnityEvent<ItemAmount>();
    [NonSerialized] public UnityEvent<ItemAmount> OnUpdateItem = new UnityEvent<ItemAmount>();

    void Start()
    {
        _inventory = GetDefaulInventory();
        InputManager.Instance.GetInputMaster().Hand.PickUpWeapon.started += _ => TryPickupItem();
    }

    private void TryPickupItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupDistance);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Item"))
            {
                ItemData data = col.GetComponent<ItemData>();
                Item item = new(ItemsTypes.SUPPLIES, data.icon, data.itemName);
                ItemAmount itemAmount = new(item, data.amount);
                itemAmount.item.SetStartCell(GetFreeCell(itemAmount));
                AddItem(itemAmount);
                Destroy(col.gameObject);
                return;
            }
            else continue;
        }
    }

    public void AddItem(ItemAmount itemAmount)
    {
        if (!_inventory.IsAllowedByCell(itemAmount.item))
            return;

        ItemAmount newItem = _inventory.AddItem(itemAmount);
        OnAddItem.Invoke(newItem);
    }

    public void RemoveItem(ItemAmount itemAmount)
    {
        _inventory.RemoveItem(itemAmount);
        OnRemoveItem.Invoke(itemAmount);
    }

    public void UpdateItem(ItemAmount itemAmount)
    {
        ItemAmount updatedItem = _inventory.UpdateItem(itemAmount);
        OnUpdateItem.Invoke(updatedItem);
    }

    private Inventory.Inventory GetDefaulInventory()
    {
        int row = 7;
        int column = 5;
        Cell[][] inventory_cells = new Cell[row][];
        for (int i = 0; i < row; ++i)
        {
            inventory_cells[i] = new Cell[column];
            for (int j = 0; j < column; ++j)
                inventory_cells[i][j] = CellsFactory.CreateCell(CellsTypes.DEFAULT);
        }

        Inventory.Inventory inventory = new Inventory.Inventory(inventory_cells);
        return inventory;
    }

    public Pair<int, int> GetShape()
    {
        return _inventory.GetShape();
    }

    public Pair<int, int> GetFreeCell(ItemAmount itemAmount)
    {
        return _inventory.GetFreeCell(itemAmount);
    }

    public int ItemCount(string itemName)
    {
        return _inventory.ItemCount(itemName);
    }

    public ItemAmount GetItem(string itemName)
    {
        return _inventory.GetItem(itemName);
    }
}

using System.Collections.Generic;
using System;

namespace Inventory
{
    [Serializable]
    public struct ItemAmount
    {
        public Item item;
        public int amount;

        public ItemAmount(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }

    [System.Serializable]
    public class Inventory: IItemContainer
    {
        private List<ItemAmount> _items;
        private Cell[][] _inventory;

        public Inventory(Cell[][] inventory)
        {
            _items = new List<ItemAmount>();
            _inventory = inventory;
        }
        public Inventory() { }

        public ItemAmount AddItem(ItemAmount _itemAmount)
        {
            ItemAmount updatedItem = UpdateItem(_itemAmount);
            if (updatedItem.amount != _itemAmount.amount)
            {
                return updatedItem;
            }
            _items.Add(_itemAmount);
            return _itemAmount;
        }
        public ItemAmount RemoveItem(ItemAmount _itemAmount)
        {
            _items.Remove(_itemAmount);
            return _itemAmount;
        }

        public ItemAmount UpdateItem(ItemAmount itemAmount)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (itemAmount.item.Name == _items[i].item.Name)
                {
                    _items[i] = new(itemAmount.item, itemAmount.amount + _items[i].amount);
                    return _items[i];
                }
            }
            return itemAmount;
        }


        public bool IsAllowedByCell(Item item)
        {
            Pair<int, int> cell = item.StartCell;
            return _inventory[cell.First][cell.Second].IsPossibleToStore(item.Type);
        }

        public Pair<int, int> GetShape()
        {
            return new(_inventory.Length, _inventory[0].Length);
        }

        public Pair<int, int> GetFreeCell(ItemAmount itemAmount)
        {
            List<Pair<int, int>> occupiedPlaces = new();
            foreach (ItemAmount item in _items)
            {
                if(itemAmount.item.Name == item.item.Name)
                {
                    return item.item.StartCell;
                }
                occupiedPlaces.Add(item.item.StartCell);
            }
            for (int i = 0; i < _inventory.Length; i++)
            {
                for (int j = 0; j < _inventory[0].Length; j++)
                {
                    Pair<int, int> currentPlace = new(i, j);
                    if (!occupiedPlaces.Contains(currentPlace))
                    {
                        return currentPlace;
                    }
                }
            }
            return new(-1, -1);
        }

        public int ItemCount(string itemName)
        {
            foreach(ItemAmount item in _items)
            {
                if(item.item.Name == itemName)
                {
                    return item.amount;
                }
            }
            return 0;
        }

        public ItemAmount GetItem(string itemName)
        {
            foreach (ItemAmount item in _items)
            {
                if (item.item.Name == itemName)
                {
                    return item;
                }
            }
            return new();
        }
    }
}

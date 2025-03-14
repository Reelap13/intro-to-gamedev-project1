using System;
using System.Collections.Generic;

namespace Inventory
{
    [Serializable]
    public class Cell
    {
        private readonly CellsTypes _type;
        private readonly List<ItemsTypes> _available_for_storage_items;

        public Cell(CellsTypes type, List<ItemsTypes> available_for_storage_items)
        {
            _type = type;
            _available_for_storage_items = available_for_storage_items;
        }
        public Cell(CellsTypes type) : this(type, new List<ItemsTypes>()) { }
        public Cell() : this(CellsTypes.UNKNOWN) { }

        public bool IsPossibleToStore(ItemsTypes item)
        {
            return _available_for_storage_items.Contains(item);
        }

        public CellsTypes Type { get { return _type; } }
    }
}

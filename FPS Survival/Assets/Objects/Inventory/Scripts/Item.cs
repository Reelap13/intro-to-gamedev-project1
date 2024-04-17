using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class Item
    {
        private ItemsTypes _type;
        private Pair<int, int> _cell;
        private string _name;
        private Sprite _icon;

        public Item(ItemsTypes type, Sprite icon, string name)
        {
            _type = type;
            _icon = icon;
            _name = name;
        }
        public Item()
        {
            _type = ItemsTypes.UNKNOWN;
        }

        public void SetStartCell(Pair<int, int> cell)
        {
            _cell = cell;
        }

        public ItemsTypes Type { get { return _type; } }
        public Pair<int, int> StartCell { get { return _cell; } }
        public string Name { get { return _name; } }
        public Sprite Icon { get { return _icon; } }
    }
}

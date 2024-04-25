using System.Collections.Generic;

namespace Inventory
{
    public class CellsFactory
    {
        public static Cell CreateCell(CellsTypes type)
        {
            List<ItemsTypes> available_for_storage_items = new List<ItemsTypes>();
            switch (type)
            {
                case CellsTypes.WEAPON:
                    break;
                case CellsTypes.DEFAULT:
                    available_for_storage_items.Add(ItemsTypes.SUPPLIES);
                    break;
                case CellsTypes.UNKNOWN:
                default:
                    break;
            }

            Cell cell = new Cell(type, available_for_storage_items);
            return cell;
        } 
    }
}

namespace Inventory
{
    public interface IItemContainer
    {
        ItemAmount AddItem(ItemAmount itemAmount);
        ItemAmount RemoveItem(ItemAmount itemAmount);
        ItemAmount UpdateItem(ItemAmount itemAmount);
}
}

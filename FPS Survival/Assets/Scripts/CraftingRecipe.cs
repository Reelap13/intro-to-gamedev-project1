using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct NameAmount
{
    public string name;
    public int amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<NameAmount> Materials;
    public List<GameObject> Results;

    public bool CanCraft(PlayerInventoryController controller)
    {
        foreach(NameAmount material in Materials)
        {
            if(controller.ItemCount(material.name) < material.amount)
            {
                return false;
            }
        }
        return true;
    }

    public void Craft(PlayerInventoryController controller)
    {
        if (CanCraft(controller))
        {
            foreach(NameAmount item in Materials)
            {
                if(controller.ItemCount(item.name) == item.amount)
                {
                    controller.RemoveItem(controller.GetItem(item.name));
                }
                else
                {
                    controller.UpdateItem(new(new(Inventory.ItemsTypes.SUPPLIES, null, item.name), -item.amount));
                }
            }

            foreach(GameObject item in Results)
            {
                Instantiate(item, controller.transform.position, controller.transform.rotation, null);
            }
        }
    }
}

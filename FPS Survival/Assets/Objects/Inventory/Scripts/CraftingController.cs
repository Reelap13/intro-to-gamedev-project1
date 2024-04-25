using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private CraftingRecipe[] recipes;
    [SerializeField] private PlaceResources placeResources;
    [SerializeField] private Image resultIcon;
    [SerializeField] private PlayerInventoryController controller;
    [SerializeField] private GameObject craftingMenu;

    private int _index;

    public void Start()
    {
        InputManager.Instance.GetInputMaster().Inventory.OpenCloseCraftingMenu.started += _ => OpenCloseMenu();
        _index = 0;
        ShowRecipe(_index);
    }

    public void ShowRecipe(int index)
    {
        _index = index;
        placeResources.materials = recipes[index].Materials;
        placeResources.PlaceResourcesFunc();
        resultIcon.sprite = recipes[index].Results[0].GetComponent<Weapon>().icon;
    }

    public void TryCraft()
    {
        recipes[_index].Craft(controller);
    }

    private void OpenCloseMenu()
    {
        if (craftingMenu.gameObject.activeSelf)
        {
            craftingMenu.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            InputManager.Instance.GetInputMaster().Hand.Enable();
            InputManager.Instance.GetInputMaster().Attack.Enable();
        }
        else
        {
            foreach (GameObject item in items)
            {
                item.GetComponentInChildren<Text>().text = controller.ItemCount(item.name).ToString();
            }
            craftingMenu.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            InputManager.Instance.GetInputMaster().Hand.Disable();
            InputManager.Instance.GetInputMaster().Attack.Disable();
        }

    }
}

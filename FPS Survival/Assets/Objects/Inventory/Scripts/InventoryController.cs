using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private PlayerInventoryController controller;
    private Slot[][] slots;
    private Pair<int, int> shape;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform parent;
    //Start is called before the first frame update
    void Start()
    {
        controller.OnAddItem.AddListener(AddItem);
        controller.OnRemoveItem.AddListener(RemoveItem);
        controller.OnUpdateItem.AddListener(UpdateItem);
        InputManager.Instance.GetInputMaster().Inventory.OpenCloseInventory.started += _ => OpenCloseInventory();

        shape = controller.GetShape();

        slots = new Slot[shape.First][];
        for (int i = 0; i < shape.First; ++i)
        {
            slots[i] = new Slot[shape.Second];
            for (int j = 0; j < shape.Second; ++j)
            {
                GameObject _slot = Instantiate(slotPrefab, parent);
                float side = _slot.GetComponent<RectTransform>().rect.width;
                _slot.GetComponent<RectTransform>().anchoredPosition = new((i - shape.First/2)* side, (shape.Second/2 - j)* side - 25);
                slots[i][j] = _slot.GetComponent<Slot>();
            }
        }
    }

    private void AddItem(ItemAmount itemAmount)
    {
        int row = itemAmount.item.StartCell.First;
        int col = itemAmount.item.StartCell.Second;
        slots[row][col].SetAmount(itemAmount.amount);
        slots[row][col].SetIcon(itemAmount.item.Icon);
        slots[row][col].SetName(itemAmount.item.Name);
    }

    private void RemoveItem(ItemAmount itemAmount)
    {
        int row = itemAmount.item.StartCell.First;
        int col = itemAmount.item.StartCell.Second;
        slots[row][col].ResetAll();
    }

    private void UpdateItem(ItemAmount itemAmount)
    {
        int row = itemAmount.item.StartCell.First;
        int col = itemAmount.item.StartCell.Second;
        slots[row][col].SetAmount(itemAmount.amount);
    }

    private void OpenCloseInventory()
    {
        if (parent.gameObject.activeSelf)
        {
            parent.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            InputManager.Instance.GetInputMaster().Hand.Enable();
            InputManager.Instance.GetInputMaster().Attack.Enable();
        }
        else
        {
            parent.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            InputManager.Instance.GetInputMaster().Hand.Disable();
            InputManager.Instance.GetInputMaster().Attack.Disable();
        }
        
    }
}

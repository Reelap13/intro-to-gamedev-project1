using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private List<Slot> slots;
    [SerializeField] private List<Weapon> weapons;

    // Update is called once per frame
    void Update()
    {
        weapons = player.HandController.Weapons;
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ResetAll();
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            slots[i].SetIcon(weapons[i].icon);
        }
    }
}

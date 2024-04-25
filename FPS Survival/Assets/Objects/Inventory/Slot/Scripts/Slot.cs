using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text amount;
    [SerializeField] private Text itemName;

    public void SetIcon(Sprite _icon)
    {
        icon.sprite = _icon;
        icon.color = Color.white;
    }

    public void SetAmount(int _amount)
    {
        amount.text = _amount.ToString();
    }

    public void SetName(string _name)
    {
        itemName.text = _name;
    }

    public void ResetAll()
    {
        icon.sprite = null;
        icon.color = Color.clear;
        amount.text = null;
        itemName.text = null;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAmmos : MonoBehaviour
{
    public HandController controller;
    private Text text;
    private Magazine magazine;

    void Start()
    {
        text = GetComponent<Text>();
        magazine = controller.weapon.GetComponent<Magazine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.weapon != null)
        {
            magazine = controller.weapon.GetComponent<Magazine>();
        }
        text.text = magazine.CurrentAmmo.ToString() + "/" + magazine.MaxAmmoCapacity.ToString();
    }
}

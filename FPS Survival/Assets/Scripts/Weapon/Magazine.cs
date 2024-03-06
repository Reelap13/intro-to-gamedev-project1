using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magazine : MonoBehaviour
{

    public Action<int> OnMagazineChanged;

    public int MaxAmmoCapacity
    {
        get => maxAmmoCapacity;

        set => maxAmmoCapacity = value > 0 ? value : 0;

    }

    public int CurrentAmmo
    {
        get => currentAmmo;

        set
        {
            currentAmmo = Mathf.Clamp(value, 0, maxAmmoCapacity);
            OnMagazineChanged?.Invoke(currentAmmo);
        }
    }

    private int currentAmmo;
    [SerializeField] private int maxAmmoCapacity;

    public void Awake()
    {
        CurrentAmmo = MaxAmmoCapacity;
    }

    public void ReduceAmmo(int ammo)
    {
        CurrentAmmo -= ammo;
        Debug.Log(CurrentAmmo);
    }

    public bool IsEmpty()
    {
        return CurrentAmmo == 0;
    }
}

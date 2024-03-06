using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField, Min(0)] private int maxCapacity;

    public void Start()
    {
        weapons.Capacity = maxCapacity;
    }

    public void AddWeapon(Weapon _weapon)
    {
        weapons.Add(_weapon);
    }

    public void RemoveWeapon(Weapon _weapon)
    {
        weapons.Remove(_weapon);
    }

    public void ReplaceActiveWeapon(Weapon _weapon)
    {
        Weapon activeWeapon = weapon;
        weapon = _weapon;
        int idx = weapons.IndexOf(_weapon);
        weapons[idx] = activeWeapon;


    }

    public void Shoot()
    {
        weapon.PerformAttack();
    }
}

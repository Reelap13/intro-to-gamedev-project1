using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Weapon defaultWeaponPref;
    private List<Weapon> weapons = new();
    [SerializeField, Min(0)] private int maxCapacity;
    [SerializeField] private float pickupDistance;
    [SerializeField] private float throwForce;
    [SerializeField] private ForceMode throwForceMode;

    private Weapon weapon;
    private void Awake()
    {
        CreateDefaultWeapon();
    }

    public void Operate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count >= 1)
        {
            ReplaceActiveWeapon(weapons[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count >= 2)
        {
            ReplaceActiveWeapon(weapons[1]);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupWeapon();
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            RemoveCurrentWeapon();
        }

    }

    private void TryPickupWeapon()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupDistance);

        foreach (Collider col in hitColliders)
        {
            Weapon newWeapon = col.GetComponent<Weapon>();
            if (newWeapon == null || newWeapon.enabled) continue;
            if (newWeapon != null && !weapons.Contains(newWeapon))
            {
                AddWeapon(newWeapon);
                return;
            }
        }
    }

    public void AddWeapon(Weapon _weapon)
    {
        _weapon.SetPreset(GetComponent<InputManager>());

        _weapon.enabled = true;
        _weapon.transform.SetParent(Camera.main.transform);
        _weapon.transform.localPosition = _weapon.offset;
        _weapon.transform.rotation = Camera.main.transform.rotation;
        _weapon.GetComponent<Rigidbody>().isKinematic = true;
        if (weapon == null)
        {
            _weapon.gameObject.SetActive(true);
            weapon = _weapon;
        }
        else
        {
            _weapon.gameObject.SetActive(false);
            weapons.Add(_weapon);
        }
    }

    public void RemoveCurrentWeapon()
    {
        if (weapon == null) return;
        weapon.gameObject.SetActive(true);
        weapon.enabled = false;
        weapon.GetComponent<Rigidbody>().isKinematic = false;
        weapon.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, throwForceMode);
        weapon.transform.SetParent(null);
        weapon = null;
    }

    public void ReplaceActiveWeapon(Weapon _weapon)
    {
        if(weapon == null)
        {
            _weapon.gameObject.SetActive(true);
            weapon = _weapon;
            weapons.Remove(_weapon);
            return;

        }
        _weapon.gameObject.SetActive(true);
        weapon.gameObject.SetActive(false);
        Weapon activeWeapon = weapon;
        weapon = _weapon;

        if (weapons.Contains(_weapon))
        {
            weapons[weapons.IndexOf(_weapon)] = activeWeapon;
        }
        else
        {
            weapons.Add(activeWeapon);
        }
    }

    private void CreateDefaultWeapon()
    {
        GameObject defaultWeapon = Instantiate(defaultWeaponPref.gameObject) as GameObject;
        AddWeapon(defaultWeapon.GetComponent<Weapon>());
    }
}

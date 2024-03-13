using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Magazine))]
[RequireComponent(typeof(Reload))]
public class Weapon : MonoBehaviour
{
    public InputManager inputManager;
    private Attack attack;
    private Magazine magazine;
    private Reload reload;
    public Vector3 offset;

    private void Start()
    {
        attack = GetComponent<Attack>();
        reload = GetComponent<Reload>();
        magazine = GetComponent<Magazine>();
        inputManager.inputMaster.Attack.Fire.started += _ => PerformAttack();
        inputManager.inputMaster.Attack.Reload.started += _ => Reload();
        magazine.OnMagazineChanged += ReloadNotify;
    }

    public void PerformAttack()
    {
        if (magazine.IsEmpty()) return;
        if (!enabled || !gameObject.activeSelf) return;
        attack.PerformAttack();
        magazine.ReduceAmmo(1);
    }

    public void Reload()
    {
        if (!enabled || !gameObject.activeSelf) return;
        reload.PerformReload(magazine);
    }

    public void ReloadNotify(int currentAmmo)
    {
        if(currentAmmo == 0)
        {
            Debug.Log("Please, reload");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Magazine))]
[RequireComponent(typeof(Reload))]
public class Weapon : MonoBehaviour
{
    private InputManager inputManager;
    private Attack attack;
    private Magazine magazine;
    private Reload reload;

    public Sprite icon;
    public Vector3 offset;

    private void OnEnable()
    {
        attack = GetComponent<Attack>();
        reload = GetComponent<Reload>();
        magazine = GetComponent<Magazine>();
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
    public void SetPreset(InputManager input)
    {
        if (inputManager != null)
        {
            inputManager.inputMaster.Attack.Fire.started -= _ => PerformAttack();
            inputManager.inputMaster.Attack.Reload.started -= _ => Reload();
        }

        inputManager = input;
        inputManager.inputMaster.Attack.Fire.started += _ => PerformAttack();
        inputManager.inputMaster.Attack.Reload.started += _ => Reload();
    }
}

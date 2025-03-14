using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    public void PerformReload(Magazine magazine)
    {
        magazine.CurrentAmmo = magazine.MaxAmmoCapacity;
    }
}

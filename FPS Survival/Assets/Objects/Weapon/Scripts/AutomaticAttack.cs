using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticAttack : Attack
{
    [SerializeField] private float coolDown;
    public override void PerformAttack()
    {
        StartCoroutine(nameof(Shoot));
    }

    private IEnumerator Shoot()
    {
        while(InputManager.Instance.GetInputMaster().Attack.Fire.ReadValue<float>() != 0)
        {
            if (!GetComponent<Magazine>().IsEmpty() && GetComponent<Weapon>().enabled && gameObject.activeSelf)
            {
                GetComponent<Magazine>().ReduceAmmo(1);
                var projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(launchPoint.forward * force, forceMode);
                yield return new WaitForSeconds(coolDown);
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

    }
}

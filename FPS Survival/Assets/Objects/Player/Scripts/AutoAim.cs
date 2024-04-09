using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent (typeof(MultiAimConstraint))]
public class AutoAim : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private MultiAimConstraintData aimConstraint;
    [SerializeField] private LayerMask layersToIgnore;

    public WeightedTransform hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        aimConstraint = GetComponent<MultiAimConstraint>().data;
        mainCamera = Camera.main;
        hitPoint = new();
        hitPoint.transform = transform;
        hitPoint.weight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Ray ray = mainCamera.ScreenPointToRay(screenCenter);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layersToIgnore))
            {
                Vector3 hitPosition = hit.point;
                hitPoint.transform.position = hitPosition;
                //if (Vector3.Distance(hitPosition, transform.position) > 1f)
                //{
                //    hitPoint.transform.position = hitPosition;
                //}
                
            }
            else
            {
                Vector3 rayDirection = ray.direction;
                Vector3 hitPosition = ray.origin + rayDirection * 100f;
                hitPoint.transform.position = hitPosition;
            }
        }
    }
}

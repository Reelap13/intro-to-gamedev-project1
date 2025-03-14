using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject hint;

    [SerializeField] private Vector3 activePosition;
    [SerializeField] private Vector3 passivePosition;

    [SerializeField] private HandController hand;

    // Update is called once per frame
    void Update()
    {
        if(hand.weapon == null)
        {
            target.transform.localPosition = passivePosition;
        }
        else
        {
            target.transform.localPosition = activePosition;
        }
    }
}

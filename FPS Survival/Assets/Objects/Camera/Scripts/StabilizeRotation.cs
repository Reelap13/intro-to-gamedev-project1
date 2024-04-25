using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizeRotation : MonoBehaviour
{

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

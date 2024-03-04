using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{

    private PlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.Move();
    }
}

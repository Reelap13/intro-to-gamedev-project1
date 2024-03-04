using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthSystem))]
public class Player : MonoBehaviour
{

    private PlayerMovement movement;
    private HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnHealthChanged += TryInvokeDeath;
    }

    // Update is called once per frame
    void Update()
    {
        movement.Move();

    }

    void TryInvokeDeath(int health)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

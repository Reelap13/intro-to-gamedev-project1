using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(HandController))]
public class Player : MonoBehaviour
{
    [NonSerialized] public UnityEvent OnPlayerDieing = new UnityEvent();
    private PlayerMovement movement;
    private HealthSystem healthSystem;
    private HandController handController;

    public PlayerMovement Movement {get => movement;}
    public HealthSystem HealthSystem {get => healthSystem;}
    public HandController HandController {get => handController;}

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        healthSystem = GetComponent<HealthSystem>();
        handController = GetComponent<HandController>();
        healthSystem.OnHealthChanged.AddListener(TryInvokeDeath);
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
            OnPlayerDieing.Invoke();
            Destroy(gameObject);
        }
    }
}

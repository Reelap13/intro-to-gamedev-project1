using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingTest : MonoBehaviour
{
    public GameObject agentPrefab;
    public int flockSize = 20;
    public float neighborRadius = 5f;
    public float separationDistance = 2f;
    public float fovAngle = 90f; // Field of View in degrees
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    public Vector2 spawnBounds = new Vector2(10f, 10f);

    private List<Transform> flock = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-spawnBounds.x, spawnBounds.x), Random.Range(-spawnBounds.y, spawnBounds.y));
            GameObject newAgent = Instantiate(agentPrefab, randomPosition, Quaternion.identity);

            // Initialize Vehicle
            newAgent.transform.position = new(randomPosition.x, 2, randomPosition.y);
            newAgent.transform.localEulerAngles = new(0, Random.Range(-180, 180), 0);

            flock.Add(newAgent.transform);
        }
    }

    void Update()
    {
        foreach (Transform agent in flock)
        {

            // Calculate flocking forces
            Vector2 cohesion = FlockingAlgorithm.Cohesion(agent, flock, neighborRadius, fovAngle,flock[0], 100);
            Vector2 alignment = FlockingAlgorithm.Alignment(agent, flock, neighborRadius, fovAngle, flock[0], 100);
            Vector2 separation = FlockingAlgorithm.Separation(agent, flock, separationDistance, fovAngle, flock[0]);

            Vector2 target = (cohesion + alignment + separation).normalized;

            if(target != Vector2.zero)
            {
                agent.transform.rotation = Quaternion.LookRotation(new(target.x, 0, target.y));
            }

            // Update position
            agent.position += agent.forward * moveSpeed * Time.deltaTime;


            //Wrap agent around the screen
            if (agent.position.x > spawnBounds.x) agent.position = new(-spawnBounds.x, agent.position.y, agent.position.z);
            if (agent.position.x < -spawnBounds.x) agent.position = new(spawnBounds.x, agent.position.y, agent.position.z);
            if (agent.position.z > spawnBounds.y) agent.position = new(agent.position.x, agent.position.y, -spawnBounds.y);
            if (agent.position.z < -spawnBounds.y) agent.position = new(agent.position.x, agent.position.y, spawnBounds.y);
        }
    }
}
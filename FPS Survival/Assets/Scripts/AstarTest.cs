using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarTest : MonoBehaviour
{
    public Vector3Int startPosition;
    public Vector3Int goalPosition;
    public float stepSize = 1;
    public LayerMask obstacleLayer;

    public List<Vector3Int> path; // Store the path for visualization

    void Start()
    {
        path = AStarAlgorithm.AStarPathfinding(startPosition, goalPosition, stepSize, obstacleLayer);

        if (path != null && path.Count > 0)
        {
            Debug.Log("Path found with " + path.Count + " steps.");
            // You can now use the 'path' list to move your agent.
        }
        else
        {
            Debug.Log("No path found.");
        }
    }

    // Draw the path in the editor for visualization
    void OnDrawGizmos()
    {
        if (path != null && path.Count > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}

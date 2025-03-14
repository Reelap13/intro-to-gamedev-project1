using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlgorithm
{
    public class Node : IComparable<Node>
    {
        public Vector3 position;
        public float cost;
        public float heuristic;
        public Node parent;

        public Node(Vector3 position)
        {
            this.position = position;
            cost = 0.0f;
            heuristic = 0.0f;
            parent = null;
        }

        public int CompareTo(Node other)
        {
            if (other == null) return 1;

            float f1 = cost + heuristic;
            float f2 = other.cost + other.heuristic;

            if (f1 > f2)
                return 1;
            else if (f1 < f2)
                return -1;
            else
                return 0;
        }
    }
    public static float ComputeHeuristic(Node node, Node goal)
    {
        return Vector3.Distance(node.position, goal.position);
    }

    public static List<Vector3> GetNeighbors(Vector3 currentPosition, float stepSize, LayerMask obstacleLayer)
    {
        List<Vector3> neighbors = new List<Vector3>();

        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            (Vector3.forward + Vector3.right).normalized,
            (Vector3.forward + Vector3.left).normalized,
            (Vector3.back + Vector3.right).normalized,
            (Vector3.back + Vector3.left).normalized
        };

        foreach (Vector3 direction in directions)
        {
            Vector3 newPosition = currentPosition + direction * stepSize;
            Vector3 rayDirection = direction.normalized;

            RaycastHit hit;
            if (!Physics.Raycast(currentPosition, rayDirection, out hit, stepSize, obstacleLayer))
            {
                neighbors.Add(newPosition);
            }
        }

        return neighbors;
    }

    public static List<Vector3> ReconstructPathVector3(Node goalNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node current = goalNode;

        while (current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    public static bool IsValid(int x, int y, int gridWidth, int gridHeight)
    {
        return (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight);
    }

    public static List<Vector3> AStarPathfinding(Vector3 startPosition, Vector3 goalPosition, float stepSize, LayerMask obstacleLayer)
    {
        Node start = new Node(startPosition);
        Node goal = new Node(goalPosition);

        SortedSet<Node> openSet = new SortedSet<Node>();
        start.heuristic = ComputeHeuristic(start, goal);
        openSet.Add(start);

        Dictionary<Vector3, float> visited = new Dictionary<Vector3, float>();
        visited[startPosition] = 0.0f;

        while (openSet.Count > 0)
        {
            Node current = openSet.Min;
            openSet.Remove(current);

            if (Vector3.Distance(current.position, goalPosition) < stepSize / 2)
            {
                return ReconstructPathVector3(current);
            }

            visited[current.position] = current.cost;

            List<Vector3> neighbors = GetNeighbors(current.position, stepSize, obstacleLayer);

            foreach (Vector3 neighborPos in neighbors)
            {
                float newCost = current.cost + Vector3.Distance(current.position, neighborPos);

                Node neighbor = new Node(neighborPos);

                bool neighborVisited = visited.ContainsKey(neighborPos);
                bool newCostIsBetter = (!neighborVisited || newCost < visited[neighborPos]);

                if (!neighborVisited || newCostIsBetter)
                {
                    neighbor.cost = newCost;
                    neighbor.heuristic = ComputeHeuristic(neighbor, goal);
                    neighbor.parent = current;
                    openSet.Add(neighbor);
                    visited[neighborPos] = newCost;
                }
            }
        }

        return new List<Vector3>();
    }
}

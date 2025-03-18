using System;
using System.Collections.Generic;
using UnityEngine;
using LevelGenerator.PerlinNoiseGenerator;

public class AStarAlgorithm: MonoBehaviour
{
    [SerializeField] private ObstaclesMap obstaclesMap;
    public class Node
    {
        public Vector3Int position;
        public int cost;
        public int heuristic;
        public Node parent;

        public Node(Vector3Int position)
        {
            this.position = position;
            cost = 0;
            heuristic = 0;
            parent = null;
        }
    }
    public int ComputeHeuristic(Vector3Int node, Vector3Int goal)
    {
        return Mathf.Max(Mathf.Abs(node.x - goal.x), Mathf.Abs(node.z - goal.z));
    }

    private bool IsValidCell(int x, int z, int width, int height)
    {
        return x >= 0 && z >= 0 && x < width && z < height;
    }

    public List<Vector3Int> GetNeighbors(Vector3Int currentPosition)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int x = currentPosition.x + i;
                int z = currentPosition.z + j;
                if (IsValidCell(x, z, obstaclesMap.Generator.Width, obstaclesMap.Generator.Height) && !obstaclesMap.Obstacles[x, z])
                {
                    neighbors.Add(new(x, currentPosition.y, z));
                }

            }
        }

        return neighbors;
    }

    public List<Vector3Int> ReconstructPathVector3(Node goalNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node current = goalNode;

        while (current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    public List<Vector3Int> AStarPathfinding(Vector3Int startPosition, Vector3Int goalPosition)
    {
        Node start = new Node(startPosition);
        Node goal = new Node(goalPosition);

        List<Node> openSet = new List<Node>();
        start.heuristic = ComputeHeuristic(start.position, goal.position);
        openSet.Add(start);

        Dictionary<Vector3Int, int> visited = new Dictionary<Vector3Int, int>();

        while (openSet.Count > 0)
        {
            if (visited.Count > 1000)
            {
                return null;
            }


            Node current = FindLowestFCostNode(openSet);
            openSet.Remove(current);

            if (current.position == goalPosition)
            {
                return ReconstructPathVector3(current);
            }

            visited[current.position] = current.cost;

            List<Vector3Int> neighbors = GetNeighbors(current.position);

            foreach (Vector3Int neighborPos in neighbors)
            {
                int newCost = current.cost + 1;

                bool neighborVisited = visited.ContainsKey(neighborPos);
                int visitedCost = neighborVisited ? visited[neighborPos] : int.MaxValue;
                bool newCostIsBetter = (newCost < visitedCost);

                if (newCostIsBetter || !neighborVisited)
                {
                    Node neighbor = new Node(neighborPos)
                    {
                        cost = newCost,
                        heuristic = ComputeHeuristic(neighborPos, goal.position),
                        parent = current
                    };

                    int existingIndex = FindNodeIndexInOpenSet(openSet, neighborPos);
                    if (existingIndex == -1)
                    {
                        openSet.Add(neighbor);
                    }
                    else
                    {
                        if (newCost < openSet[existingIndex].cost)
                        {
                            openSet[existingIndex] = neighbor;
                        }
                    }
                }
            }
        }

        return new List<Vector3Int>();
    }

    private Node FindLowestFCostNode(List<Node> openSet)
    {
        if (openSet.Count == 0)
        {
            return null;
        }

        Node lowestCostNode = openSet[0];
        for (int i = 1; i < openSet.Count; i++)
        {
            if (openSet[i].cost + openSet[i].heuristic < lowestCostNode.cost + lowestCostNode.heuristic)
            {
                lowestCostNode = openSet[i];
            }
        }

        return lowestCostNode;
    }

    private int FindNodeIndexInOpenSet(List<Node> openSet, Vector3Int position)
    {
        for (int i = 0; i < openSet.Count; i++)
        {
            if (openSet[i].position == position)
            {
                return i;
            }
        }
        return -1;
    }
}

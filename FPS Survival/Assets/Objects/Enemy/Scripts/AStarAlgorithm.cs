using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlgorithm
{
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
    public static int ComputeHeuristic(Vector3Int node, Vector3Int goal)
    {
        return Mathf.Max(Mathf.Abs(node.x - goal.x), Mathf.Abs(node.z - goal.z));
    }

    public static List<Vector3Int> GetNeighbors(Vector3Int currentPosition, float stepSize, LayerMask obstacleLayer)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            (Vector3.forward + Vector3.left),
            (Vector3.forward + Vector3.right),
            (Vector3.back + Vector3.left),
            (Vector3.back + Vector3.right)
        };

        foreach (Vector3 direction in directions)
        {
            Vector3 newPosition = currentPosition + direction * stepSize;

            Vector3 raycastStart = currentPosition;
            Vector3 raycastEnd = newPosition;
            Vector3 rayDirection = (raycastEnd - raycastStart).normalized;

            RaycastHit hit;

            if (!Physics.Raycast(raycastStart, rayDirection, out hit, stepSize, obstacleLayer))
            {
                neighbors.Add(new((int)newPosition.x, (int)newPosition.y, (int)newPosition.z));
            }
        }

        return neighbors;
    }

    public static List<Vector3Int> ReconstructPathVector3(Node goalNode)
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

    public static List<Vector3Int> AStarPathfinding(Vector3Int startPosition, Vector3Int goalPosition, float stepSize, LayerMask obstacleLayer)
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

            List<Vector3Int> neighbors = GetNeighbors(current.position, stepSize, obstacleLayer);

            foreach (Vector3Int neighborPos in neighbors)
            {
                int newCost = current.cost + ComputeHeuristic(current.position, neighborPos);

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

    private static Node FindLowestFCostNode(List<Node> openSet)
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

    private static int FindNodeIndexInOpenSet(List<Node> openSet, Vector3Int position)
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

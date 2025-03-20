using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private List<WayPoint> _neighbors = new();

    public void AddNeighbor(WayPoint neighbor)
    {
        _neighbors.Add(neighbor);
    }

    public WayPoint GetNeighbor() { return GetNeighbor(null); }
    public WayPoint GetNeighbor(WayPoint previous_point)
    {
        if (previous_point == null) 
            return _neighbors[Random.Range(0, _neighbors.Count)];

        List<WayPoint> temp = new(_neighbors);
        temp.Remove(previous_point);
        return temp[Random.Range(0, temp.Count - 1)];
    }
}

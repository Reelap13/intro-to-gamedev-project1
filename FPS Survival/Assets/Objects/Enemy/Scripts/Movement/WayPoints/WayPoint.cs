using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _neighbors = new();

    public void AddNeighbor(WayPoint neighbor)
    {
        _neighbors.Add(neighbor);
    }

    public WayPoint GetNeighbor() { return GetNeighbor(null); }
    public WayPoint GetNeighbor(WayPoint previous_point)
    {
        if (previous_point == null) 
            return _neighbors[Random.Range(0, _neighbors.Count)];
        if (_neighbors.Count == 1)
            return _neighbors[0];

        List<WayPoint> temp = new(_neighbors);
        temp.Remove(previous_point);
        return temp[Random.Range(0, temp.Count - 1)];
    }
}

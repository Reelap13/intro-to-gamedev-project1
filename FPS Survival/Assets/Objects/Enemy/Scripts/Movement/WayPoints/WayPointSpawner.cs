using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class WayPointSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private WayPoint _point_prefab;
    [SerializeField] private Transform _directory;

    private List<WayPoint> _points = new();
    private Terrain _terrain;

    private void Awake()
    {
        _spawner.OnEnemySpawning.AddListener(InitEnemyWayPoints);
    }

    public void GeneratePoints(Terrain Terrain)
    {
        _terrain = Terrain;

        _points.Add(CreateWayPoint(new(95, 0, 126)));
        _points.Add(CreateWayPoint(new(54, 0, 170)));
        _points.Add(CreateWayPoint(new(57, 0, 128)));
        _points.Add(CreateWayPoint(new(31, 0, 94)));

        ConnectWayPoints(_points[0], _points[1]);
        ConnectWayPoints(_points[1], _points[2]);
        ConnectWayPoints(_points[2], _points[3]);
        ConnectWayPoints(_points[3], _points[0]);
    }

    private void InitEnemyWayPoints(Enemy enemy)
    {
        WayPoint point = _points[Random.Range(0, _points.Count)];
        enemy.Transform.position = point.transform.position;
        enemy.Movement.SetStartWayPoint(point, point.GetNeighbor());
    }

    private WayPoint CreateWayPoint(Vector3 position)
    {
        WayPoint point = Instantiate(_point_prefab);
        position.y = _terrain.SampleHeight(position);
        point.transform.position = position;
        point.transform.parent = _directory;
        return point;
    }

    private void ConnectWayPoints(WayPoint a, WayPoint b)
    {
        a.AddNeighbor(b);
        b.AddNeighbor(a);
    }
}

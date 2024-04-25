using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    // использу€ сетку ¬оронова(диаграма воронова)
    public class CitiesRoadsGenerator : CitiesGeneratorElement
    {
        [SerializeField] private Vector2Int _spawning_crossroad_rate = new Vector2Int(3, 7);
        [SerializeField] private float _far_from_area = 3f;

        private Terrain _terrain => Generator.Generator.Terrain;
        private float _default_size = 2f;

        public Dictionary<City, List<Road>> GenerateCitiesRoads(List<City> cities)
        {
            Random.State previous_state = Random.state;

            Dictionary<City, List<Road>> cities_roads = new Dictionary<City, List<Road>>();
            foreach (City city in cities)
            {
                cities_roads.Add(city, GenerateCityRoads(city));
                //DrawCityRegion(city.Points);
            }

            Random.state = previous_state;

            return cities_roads;
        }

        private List<Road> GenerateCityRoads(City city)
        {
            Vector3 center = city.GetCenterPoint();
            Vector3 farthest_point = city.GetFarthestPoint(center);
            Vector3 main_road_direction = center - farthest_point;
            main_road_direction = new Vector3(main_road_direction.x, 0, main_road_direction.z).normalized * _default_size;

            Random.InitState(_seed % (int)((_offset_coefficient + new Vector2(center.x, center.z)).magnitude));

            var roads = GenerateMainRoad(city, center, main_road_direction);

            DrawCityRoads(roads);

            return roads;
        }

        private List<Road> GenerateMainRoad(City city, Vector3 center, Vector3 direction)
        {
            Road center_road = new Road(GetRoadPosition(center), direction);
            List<Road> roads = new List<Road> { center_road };

            roads.AddRange(GenerateRoadToDirection(city, center_road, direction, false));
            roads.AddRange(GenerateRoadToDirection(city, center_road, -direction, false));
            roads.AddRange(GenerateRoadToDirection(city, center_road, RotateDirectionToAngle(direction, -90), false));
            roads.AddRange(GenerateRoadToDirection(city, center_road, RotateDirectionToAngle(direction, 90), false));

            return roads;
        }

        private List<Road> GenerateRoadToDirection(City city, Road source, Vector3 direction, bool is_create_crossroad)
        {
            Random.State previous_state = Random.state;
            Random.InitState(_seed % (int)((_offset_coefficient * new Vector2(source.Position.x, source.Position.z)).magnitude));

            List<Road> roads = new List<Road>();

            Road road = new Road(GetRoadPosition(source.Position + direction), direction);
            Road.SetAdjacentRoads(source, road);

            roads.Add(road);
            int crossroad_index = GenerateDistanceToCrossroad();
            while (city.DistanceToClosestPoint(road.Position) < _far_from_area)
            {
                Road new_road = new Road(GetRoadPosition(road.Position + direction), direction);
                Road.SetAdjacentRoads(road, new_road);

                road = new_road;
                roads.Add(new_road);

                if (--crossroad_index <= 0 && is_create_crossroad)
                {
                    crossroad_index = GenerateDistanceToCrossroad();
                    roads.AddRange(GenerateRoadToDirection(city, new_road, RotateDirectionToAngle(direction, -90), false));
                    roads.AddRange(GenerateRoadToDirection(city, new_road, RotateDirectionToAngle(direction, 90), false));
                }
            }

            Random.state = previous_state;
            return roads;
        }

        private Vector3 GetRoadPosition(Vector3 point)
        {
            return new Vector3(point.x, _terrain.SampleHeight(point) + 1, point.z);
        }
        private int GenerateDistanceToCrossroad()
        {
            return Random.Range(_spawning_crossroad_rate.x, _spawning_crossroad_rate.y + 1);
        }
        private Vector3 RotateDirectionToAngle(Vector3 direction, float angle)
        {
            return Quaternion.Euler(0, angle, 0) * direction;
        }

        private void DrawCityRoads(List<Road> roads)
        {
            HashSet<Road> drowed_road = new HashSet<Road>();
            Queue<Road> next_roads = new Queue<Road>();

            drowed_road.Add(roads[0]);
            next_roads.Enqueue(roads[0]);
            while (next_roads.Count > 0)
            {
                Road road = next_roads.Dequeue();
                foreach (Road adj_road in road.AdjacentRoads)
                {
                    if (drowed_road.Contains(adj_road))
                        continue;

                    Debug.DrawLine(road.Position, adj_road.Position, Color.green, 100f);
                    drowed_road.Add(adj_road);
                    next_roads.Enqueue(adj_road);
                }
            }
        }
        private void DrawCityRegion(List<Vector3> city_region)
        {
            for (int i = 0; i < city_region.Count; ++i)
            {
                for (int j = 0; j < city_region.Count; ++j)
                {
                    Vector3 start = city_region[i];
                    Vector3 end = city_region[j];
                    if (start == end || start.x != end.x && start.z != end.z || Vector3.Distance(start, end) >= 1.1f)
                        continue;

                    Debug.DrawLine(start, end, Color.red, 100f);
                }
            }
        }

        public class Road
        {
            public Vector3 Position;
            public float Rotation;
            public List<Road> AdjacentRoads;

            public Road(Vector3 position, float rotation)
            {
                AdjacentRoads = new List<Road>();
                this.Position = position;
                this.Rotation = rotation;
            }
            public Road(Vector3 position, Vector3 direction) : this(position, Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg) { }

            public static void SetAdjacentRoads(Road a, Road b)
            {
                a.AdjacentRoads.Add(b);
                b.AdjacentRoads.Add(a);
            }

            public void Unsubscribe()
            {
                foreach (Road road in AdjacentRoads)
                    road.AdjacentRoads.Remove(this);

                AdjacentRoads.Clear();
            }
        }
    }
}
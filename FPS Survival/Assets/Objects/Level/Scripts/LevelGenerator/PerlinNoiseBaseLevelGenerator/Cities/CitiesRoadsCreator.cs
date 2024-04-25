using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator.PerlinNoiseGenerator.Cities.CitiesRoadsGenerator;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesRoadsCreator : CitiesGeneratorElement
    {
        [SerializeField] private float _max_distance_from_buildings;
        [field: SerializeField] public Texture2D RoadTexture { get; private set; }
        public FloatArray2D CitiesRoadsMap { get; private set; }

        public FloatArray2D CreateRoads(Dictionary<City, List<Road>> roads)
        {
            CitiesRoadsMap = new FloatArray2D(_width, _height);
            foreach (City city in roads.Keys)
            {
                foreach (Road road_data in roads[city])
                {
                    FillAroundPoint(road_data.Position);
                }
            }
            return CitiesRoadsMap;
        }

        private void FillAroundPoint(Vector3 point)
        {
            List<Vector2Int> points = CitiesRoadsMap.GetCellsInRange((int)point.x, (int)point.z, 2);
            foreach (var finded_point in points)
            {
                CitiesRoadsMap[finded_point.y, finded_point.x] = 1;
            }
           
        }

        public void CreateRoadsNearToBuilding(Dictionary<City, List<Road>> roads, Dictionary<City, List<Building>> buildings)
        {
            foreach (City city in roads.Keys)
            {
                List<Road> city_roads = roads[city];
                List<Building> city_buildings = buildings[city];
                List<Road> all_removed_roads = new List<Road>();
                foreach (Road road in city_roads)
                {
                    List<Road> removed_roads = RemoveEnds(road, city_buildings);
                    if (removed_roads.Count > 0)
                        all_removed_roads.AddRange(removed_roads);
                }

                if (all_removed_roads.Count > 0)
                    Debug.Log($"for city was deleted {all_removed_roads.Count} roads");
                foreach (Road removed_road in all_removed_roads)
                    city_roads.Remove(removed_road);
            }

            CreateRoads(roads);
        }
        private List<Road> RemoveEnds(Road start_road, List<Building> buildings)
        {
            List<Road> removed_roads = new List<Road>();

            Queue<Road> roads_to_Check = new Queue<Road>();
            roads_to_Check.Enqueue(start_road);

            while (roads_to_Check.Count > 0)
            {
                Road current_road = roads_to_Check.Dequeue();

                if (current_road.AdjacentRoads.Count == 1 && Building.DistanceToClosestBuilding(current_road.Position, buildings) > _max_distance_from_buildings)
                {
                    Road neighborRoad = current_road.AdjacentRoads[0];
                    current_road.Unsubscribe();
                    removed_roads.Add(current_road);
                    roads_to_Check.Enqueue(neighborRoad);
                }
            }

            return removed_roads;
        }
    }
}
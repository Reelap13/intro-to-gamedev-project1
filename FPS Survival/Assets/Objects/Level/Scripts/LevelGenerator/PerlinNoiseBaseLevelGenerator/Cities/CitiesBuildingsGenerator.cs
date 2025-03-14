using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesBuildingsGenerator : CitiesGeneratorElement
    {
        [SerializeField] private int[] _buildings = { 400, 700, 1400, 2100, 3400, 5500, 6000, 7900, 10000 };
        [SerializeField] private int _gradient_range = 8;
        [SerializeField] private Vector2 _building_range = new Vector2(0.2f, 0.4f);
        [SerializeField] private Vector2 _building_scale = new Vector2(0.5f, 2f);
        [SerializeField] private float _distance_between_buildings = 5f;
        [SerializeField] private int _building_roation_range = 2;
        [SerializeField] private float _rotation_agnle_range = 15;
        [SerializeField] private int _building_size = 4;

        private float _distance_to_city = 10f;
        private int _build_safy_size = 10;
        
        public Dictionary<City, List<Building>> Buildings { get; private set; }
        public FloatArray2D BuildingMap { get; private set; }
        public FloatArray2D GradientBuildingMap { get; private set; }
        public Dictionary<City, List<Building>> GenerateBuildings(List<City> cities, FloatArray2D roads_map)
        {
            Random.State previous_state = Random.state;
            Random.InitState(_seed % (int)((CalculateOffset().magnitude)));

            FloatArray2D gradient_cities_roads_map = FloatArray2D.GenerateGradientBiomMap(roads_map, _gradient_range, false);
            Dictionary<City, List<Vector2Int>> cities_buildings_points = GetCityBuildingPoints(cities, gradient_cities_roads_map);

            Buildings = new Dictionary<City, List<Building>>();
            BuildingMap = new FloatArray2D(_width, _height);
            foreach (var city in cities)
            {
                Buildings[city] = ProcessCity(city, cities_buildings_points[city], gradient_cities_roads_map);
            }

            Random.state = previous_state;

            GradientBuildingMap = FloatArray2D.GenerateGradientBiomMap(BuildingMap, _build_safy_size, false);
            return Buildings;
        }

        private List<Building> ProcessCity(City city, List<Vector2Int> buildings_points, FloatArray2D gradient_cities_roads_map)
        {
            List<Building> buildings = new List<Building>();

            int number = 0;
            int rand_value = (int)(city.Points.Count * Random.Range(_building_scale.x, _building_scale.y));
            for (int i = 0; i < _buildings.Length; ++i)
            {
                if (rand_value < _buildings[i])
                    break;
                ++number;
            }

            for (int i = 0; i < number; ++i)
            {
                Vector3 position = GetPosition(buildings_points, buildings);
                if (position.y != 0)
                    break;
                Vector3 parsed_position = new Vector3(position.x, Generator.Generator.Terrain.SampleHeight(position) -0.5f, position.z);
                float rotation = GetRoation(parsed_position, gradient_cities_roads_map);
                buildings.Add(new Building(parsed_position, rotation));
                BuildingMap.SetValueInRange((int)position.z, (int)position.x, _building_size, 1);
            }

            return buildings;
        }

        private float GetRoation(Vector3 position, FloatArray2D gradient_cities_roads_map)
        {
            Vector2 point = gradient_cities_roads_map.GetClosestHighestValueCoordinates((int)position.z, (int)position.x, _building_roation_range);
            Vector2 direction = new Vector3(point.x - position.z, point.y - position.x);
            return -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + Random.Range(-_rotation_agnle_range, _rotation_agnle_range);
        }

        private Vector3 GetPosition(List<Vector2Int> buildings_points, List<Building> buildings)
        {
            while (buildings_points.Count > 0)
            {
                int index = Random.Range(0, buildings_points.Count);
                Vector2Int position = buildings_points[index];
                buildings_points.RemoveAt(index);
                if (DistanceToBuildings(new Vector3(position.y, 0, position.x), buildings) > _distance_between_buildings)
                    return new Vector3(position.y, 0, position.x);
            }
            return Vector3.one;
        }
        private float DistanceToBuildings(Vector3 position, List<Building> buildings)
        {
            float min_distance = float.MaxValue;
            foreach (Building building in buildings)
            {
                float distance = Vector3.Distance(position, building.Position);
                if (distance < min_distance)
                    min_distance = distance;
            }
            return min_distance;
        }

        public Dictionary<City, List<Vector2Int>> GetCityBuildingPoints(List<City> cities, FloatArray2D gradient_road_map)
        {
            Dictionary<City, List<Vector2Int>> cities_buildings_points = new Dictionary<City, List<Vector2Int>>();

            foreach (City city in cities)
                cities_buildings_points.Add(city, new List<Vector2Int>());

            List<Vector2Int> buildings_points = gradient_road_map.GetCellsByValueInRange(_building_range.x, _building_range.y);
            foreach(var building_point in buildings_points)
            {
                foreach (City city in cities)
                {
                    if (city.DistanceToClosestPoint(building_point) < _distance_to_city)
                    {
                        cities_buildings_points[city].Add(building_point);
                        break;
                    }
                }
            }

            return cities_buildings_points;
        }
    }

    public class Building
    {
        public Vector3 Position;
        public float Rotation;

        public Building(Vector3 position, float rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public static Building GetClosestBuilding(Vector3 point, List<Building> buildings)
        {
            if (buildings.Count == 0) return null;

            Building closest_building = buildings[0];
            foreach (Building building in buildings)
                if (Vector3.Distance(point, building.Position) < Vector3.Distance(point, closest_building.Position))
                    closest_building = building;

            return closest_building;
        }
        public static float DistanceToClosestBuilding(Vector3 point, List<Building> buildings)
        {
            Building building = GetClosestBuilding(point, buildings);
            return building != null ? Vector3.Distance(point, building.Position) : Mathf.Infinity;
        }
    }
}
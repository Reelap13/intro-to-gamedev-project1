using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesPositionsGenerator : CitiesGeneratorElement
    {
        [SerializeField] private float _scale = 20f;
        [SerializeField] private float _boundary = 0.9f;
        [SerializeField] private float _min_points_number_to_create_city = 20f;

        public List<City> GenerateCitiesLocations()
        {
            FloatArray2D cities_map = GenerateCitiesMap();

            List<City> cities = DevideCitiesMapToCities(cities_map);

            return cities;
        }

        private FloatArray2D GenerateCitiesMap()
        {
            FloatArray2D cities_map = new FloatArray2D(_width, _height);
            Vector2 offset = CalculateOffset();

            for (int x = 0; x < _width; ++x)
            {
                for (int y = 0; y < _height; ++y)
                {
                    float perlinValue = Mathf.PerlinNoise((x + offset.x) / _scale, (y + offset.y) / _scale);
                    cities_map[x, y] = (perlinValue > _boundary) ? 1 : 0;
                }
            }

            return cities_map;
        }
        
        private List<City> DevideCitiesMapToCities(FloatArray2D cities_map)
        {
            List<City> cities = new List<City>();

            for(int x = 0; x < _width; ++x)
            {
                for (int y = 0; y < _height; ++y)
                {
                    if (cities_map[x, y] == 0)
                        continue;

                    List<Vector2Int> city_points = GetCityPoints(x, y, cities_map);
                    if (city_points.Count < _min_points_number_to_create_city)
                        continue;

                    City city = GenerateCityRegion(city_points);
                    cities.Add(city);
                }
            }

            return cities;
        }

        private List<Vector2Int> GetCityPoints(int x, int y, FloatArray2D cities_map)
        {
            List<Vector2Int> city_points = new List<Vector2Int>();
            Queue<Vector2Int> active_city_points = new Queue<Vector2Int>();
            

            active_city_points.Enqueue(new Vector2Int(x, y));
            while (active_city_points.Count != 0)
            {
                Vector2Int point = active_city_points.Dequeue();

                city_points.Add(point);

                foreach (Vector2Int intersecting_point in cities_map.GetCellsInRange(point.x, point.y, 1))
                {
                    if (cities_map[intersecting_point.x, intersecting_point.y] == 0)
                        continue;

                    cities_map[intersecting_point.x, intersecting_point.y] = 0;
                    active_city_points.Enqueue(intersecting_point);
                }
            }

            return city_points;
        }


        private City GenerateCityRegion(List<Vector2Int> city_points)
        {
            City city = new City();
            Terrain terrain = Generator.Generator.Terrain;

            foreach (Vector2Int point in city_points)
            {
                city.Points.Add(new Vector3(point.x, terrain.SampleHeight(new Vector3(point.x, 0, point.y)) + 1, point.y));
            }

            return city;
        }
    }

    public class City
    {
        public List<Vector3> Points = new List<Vector3>();

        public Vector3 GetCenterPoint()
        {
            Vector3 center_point = Vector3.zero;
            foreach (Vector3 point in Points)
                center_point += point / Points.Count;

            return center_point;
        }

        public Vector3 GetFarthestPoint(Vector3 point)
        {
            Vector3 farthest_point = point;
            foreach(Vector3 city_point in Points)
                if (Vector3.Distance(point, city_point) > Vector3.Distance(point, farthest_point))
                    farthest_point = city_point;

            return farthest_point;
        }

        public Vector3 GetClosestPoint(Vector3 point)
        {
            Vector3 closest_point = Vector3.positiveInfinity;
            foreach (Vector3 city_point in Points)
                if (Vector3.Distance(point, city_point) < Vector3.Distance(point, closest_point))
                    closest_point = city_point;

            return closest_point;
        }

        public float DistanceToClosestPoint(Vector3 point)
        {
            return (GetClosestPoint(point) - point).magnitude;
        }
        public float DistanceToClosestPoint(Vector2Int point)
        {
            Vector3 parsed_point = new Vector3(point.y, 0, point.x);
            Vector3 closest_point = GetClosestPoint(parsed_point);
            closest_point.y = 0;

            return Vector3.Distance(parsed_point, closest_point);
        }
    }
}
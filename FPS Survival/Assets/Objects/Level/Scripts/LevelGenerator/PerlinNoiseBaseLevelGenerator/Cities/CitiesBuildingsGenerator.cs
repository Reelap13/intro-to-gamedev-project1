using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesBuildingsGenerator : CitiesGeneratorElement
    {
        [SerializeField] private int[] _buildings = { 400, 700, 1400, 2100, 3400, 5500 };
        [SerializeField] private int _gradient_range = 5;
        [SerializeField] private Vector2 _building_range = new Vector2(0.3f, 0.13f);

        public FloatArray2D CitiesBuildingsMap { get; private set; }
        public void GenerateBuildings(Dictionary<City, List<Roads>> roads, FloatArray2D roads_map)
        {
            CitiesBuildingsMap = roads_map.GenerateGradientBiomMap(roads_map, _gradient_range, false);
            
            foreach (var road in roads)
            {
                ProcessCity(road.Key, road.Value);
            }
        }

        private List<Building> ProcessCity(City city, List<Roads> roads)
        {
            List<Building> result = new List<Building>();
            int size = city.Points.Count;

            for (int i = 0; i < _buildings.Length; ++i)
            {
                if (size < _buildings[i])
                    break;


            }

            return null;
        }

        private void Build()
        {

        }

        private List<Vector2Int> GetCityBuilding(City city)
        {

            return null;
        }

        public class Building
        {
            public Vector3 Position;
            public float Rotation;
        }
    }
}
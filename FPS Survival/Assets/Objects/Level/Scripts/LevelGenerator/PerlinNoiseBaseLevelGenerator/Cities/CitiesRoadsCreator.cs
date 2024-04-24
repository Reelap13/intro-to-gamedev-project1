using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator.PerlinNoiseGenerator.Cities.CitiesRoadsGenerator;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesRoadsCreator : CitiesGeneratorElement
    {
        [SerializeField] private GameObject _road_pref;
        [SerializeField] private GameObject _crossroad_pref;

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
    }
}
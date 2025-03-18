using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator.PerlinNoiseGenerator.Cities.CitiesRoadsGenerator;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesBuildingsCreator : CitiesGeneratorElement
    {
        [SerializeField] private GameObject[] _buildings_pref;

        public FloatArray2D CitiesBuildingsMap { get; private set; }

        public FloatArray2D CreateBuildings(Dictionary<City, List<Building>> buildings)
        {
            Random.State previous_state = Random.state;
            Random.InitState(_seed % (int)((CalculateOffset().magnitude)));

            CitiesBuildingsMap = new FloatArray2D(_width, _height);
            foreach (City city in buildings.Keys)
            {
                foreach (Building building in buildings[city])
                {
                    CreateBuilding(building);
                    FillAroundPoint(building.Position);
                }
            }

            Random.state = previous_state;
            return CitiesBuildingsMap;
        }

        private void CreateBuilding(Building building_data)
        {
            GameObject building = Instantiate(_buildings_pref[Random.Range(0, _buildings_pref.Length)]) as GameObject;
            building.transform.parent = Generator.Generator.LevelDirectory;
            building.transform.position = building_data.Position;
            building.transform.rotation = Quaternion.Euler(building.transform.eulerAngles + Vector3.up * building_data.Rotation);
        }

        private void FillAroundPoint(Vector3 point)
        {
            List<Vector2Int> points = CitiesBuildingsMap.GetCellsInRange((int)point.x, (int)point.z, 2);
            foreach (var finded_point in points)
            {
                CitiesBuildingsMap[finded_point.y, finded_point.x] = 1;
            }

        }
    }
}
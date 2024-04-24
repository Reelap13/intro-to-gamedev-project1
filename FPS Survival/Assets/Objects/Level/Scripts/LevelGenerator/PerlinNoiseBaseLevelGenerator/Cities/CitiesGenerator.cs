using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Cities
{
    public class CitiesGenerator : LevelGeneratorElement
    {
        [field: SerializeField]
        public CitiesPositionsGenerator PositionGenerator { get; private set; }
        [field: SerializeField]
        public CitiesRoadsGenerator RoadsGenerator { get; private set; }
        [field: SerializeField]
        public CitiesRoadsCreator RoadsCreator { get; private set; }

        public FloatArray2D GenerateCities()
        {
            List<City> cities = PositionGenerator.GenerateCitiesLocations();

            var roads =RoadsGenerator.GenerateCitiesRoads(cities);
            return RoadsCreator.CreateRoads(roads);
        }
    }
}
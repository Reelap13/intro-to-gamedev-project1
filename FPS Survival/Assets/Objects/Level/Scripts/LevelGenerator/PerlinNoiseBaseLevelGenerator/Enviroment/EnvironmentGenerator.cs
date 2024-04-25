using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Environment 
{
    public class EnvironmentGenerator : LevelGeneratorElement
    {
        [field: SerializeField]
        public BiomGenerator Biom { get; private set; }
        [field: SerializeField]
        public BiomFiller BiomFiller { get; private set; }

        public void GenerateEnvironment()
        {
            Biom.GenerateBiomMap();
            Biom.GenerateGradientBiomMap();

            BiomFiller.FillBioms();
        }
    }
}
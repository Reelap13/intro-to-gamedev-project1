using UnityEngine;

namespace LevelGenerator.PerlinNoiseGenerator.Environment
{
    public class BiomGenerator : EnvironmentGeneratorElement
    {
        [SerializeField] private float _scale = 20f;
        [SerializeField] private Texture2D _texture;
        [SerializeField] private int _gradient_range = 5;

        public FloatArray2D BiomMap { get; private set; }
        public FloatArray2D GradientBiomMap { get; private set; }
        public FloatArray2D GenerateBiomMap()
        {
            BiomMap = new FloatArray2D(_width, _height);

            for (int x = 0; x < _width; ++x)
            {
                for (int y = 0; y < _height; ++y)
                {
                    if (Generator.Generator.BlockedMap[x, y] == 1)
                        continue;

                    BiomMap[x, y] = 1;
                }
            }

            return BiomMap;
        }
        public FloatArray2D GenerateGradientBiomMap()
        {
            GradientBiomMap = FloatArray2D.GenerateGradientBiomMap(Generator.Generator.BlockedMap, _gradient_range, false);
            GradientBiomMap.ReverseValues();

            return GradientBiomMap;
        }


        public Texture2D Texture { get { return _texture; } }
    }
}

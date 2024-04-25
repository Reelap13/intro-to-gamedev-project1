using LevelGenerator.PerlinNoiseGenerator.Cities;
using LevelGenerator.PerlinNoiseGenerator.Environment;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LevelGenerator.PerlinNoiseGenerator
{
    public class PerlinNoiseLevelGenerator : LevelGenerator
    {
        [field: SerializeField]
        public TerrainHeights Heights { get; private set; }
        [field: SerializeField]
        public EnvironmentGenerator Environment { get; private set; }
        [field: SerializeField]
        public Roads Roads { get; private set; }
        [field: SerializeField]
        public CitiesGenerator Cities { get; private set; }


        [field: SerializeField]
        public Terrain Terrain { get; private set; }

        [field: SerializeField] public int Width { get; private set; } = 256;
        [field: SerializeField] public int Height { get; private set; } = 256;

        [field: SerializeField] public int Seed { get; private set; }
        
        public FloatArray2D BlockedMap { get; private set; }

        [MenuItem("MyTools/Bake Lighting")]
        public override void GenerateLevel()
        {
           
            Random.State previous_state = Random.state;
            //Seed = Random.Range(0, int.MaxValue);
            BlockedMap = new FloatArray2D(Width, Height);

            TerrainData terrain_data = Terrain.terrainData;
            terrain_data.heightmapResolution = Width + 1;
            terrain_data.size = new Vector3(Width, terrain_data.size.y, Height);

            float[,] heights = Heights.GenerateHeights();
            terrain_data.SetHeights(0, 0, heights);

            var cities_roads_map = Cities.GenerateCities();
            AddToBlockedMap(cities_roads_map);
            AddToBlockedMap(Cities.BuildingsGenerator.BuildingMap);
            var road_map = Roads.GenerateRoadMap();
            AddToBlockedMap(road_map);

            Environment.GenerateEnvironment();

            ApplyTextures(terrain_data);

            Surface.BuildNavMesh();
            Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;
            Lightmapping.Bake();
            Random.state = previous_state;
        }

        private void ApplyTextures(TerrainData terrain_data)
        {
            FloatArray2D environment_map = Environment.Biom.BiomMap;
            FloatArray2D cities_roads_map = Cities.RoadsCreator.CitiesRoadsMap;
            FloatArray2D road_map = Roads.RoadMap;
            //FloatArray2D ttt = FloatArray2D.GenerateGradientBiomMap(Cities.RoadsCreator.CitiesRoadsMap, 21, false);

            terrain_data.alphamapResolution = Width;
            float[,,] splatmap_data = new float[Width, Height, 3];
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    splatmap_data[x, y, 0] = environment_map[x, y]; //environment_map[x, y];
                    splatmap_data[x, y, 1] = cities_roads_map[x, y];
                    splatmap_data[x, y, 2] = road_map[x, y];
                    /*splatmap_data[x, y, 0] = ttt[x, y]; //environment_map[x, y];
                    splatmap_data[x, y, 1] = 0.01f < ttt[x, y] && ttt[x, y] < 0.1f ? 1 : 0; ; /// cities_roads_map[x, y];
                    splatmap_data[x, y, 2] = 0;// road_map[x, y];*/
                }
            }

            terrain_data.terrainLayers = new TerrainLayer[] { 
                new TerrainLayer { diffuseTexture = Environment.Biom.Texture }, 
                new TerrainLayer { diffuseTexture = Cities.RoadsCreator.RoadTexture },
                new TerrainLayer { diffuseTexture = Roads.Texture } };

            terrain_data.SetAlphamaps(0, 0, splatmap_data);
        }

        public override Vector3 GetCenter()
        {
            return new Vector3(Width/2, Terrain.SampleHeight(new Vector3(Width/2, 0, Height/2)), Height/2);
        }

        public void AddToBlockedMap(FloatArray2D map)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Height; ++y)
                    if (map[x, y] == 1)
                        BlockedMap[x, y] = 1;
        }

        public override Vector3 GetFreePoint(Vector3 position)
        {
            Vector2Int parsed_position = Cities.BuildingsGenerator.GradientBuildingMap.GetNearestLowerValue((int)position.x, (int)position.z, 0);
            Vector3 free_position = new Vector3(parsed_position.y, 0, parsed_position.x);
            free_position.y = Terrain.SampleHeight(free_position);
            return free_position;
        }
        public override Vector3 BoardPosition(Vector3 position, float bounds)
        {
            return new Vector3(
                Mathf.Clamp(position.x, bounds, Width - bounds),
                position.y,
                Mathf.Clamp(position.z, bounds, Height - bounds));
        }
    }
}
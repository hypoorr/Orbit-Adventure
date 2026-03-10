using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int depth = 20;

    public int width = 10000;
    public int height = 10000;

    public float scale = 20f;

    //seed adds randomization to world generation
    public float seed;


    void Start()
    {

        seed = Random.Range(0f, 9999f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);


    }

    TerrainData GenerateTerrain(TerrainData terrainData) // set terrain size and generate heights
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights() // build the heights of the terrain
    {
        float[,] heights = new float[width, height];
        for (int x=0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y) // calculate the height of each part of the terrain
    {
        float xCoord = (float)x / width * scale + seed;
        float yCoord = (float)y / height * scale + seed;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{

    public float depth = 20;

    public int width = 1000;
    public int height = 1000;

    private float xTerrainPos;
    private float zTerrainPos;


    public float scale = 20f;

    [SerializeField] private GameObject shipModel;
    [SerializeField] private GameObject playerModel;

    [SerializeField] private GameObject loadingScreen;


    //seed adds randomization to world generation
    public float seed;


    void Start()
    {
        loadingScreen.SetActive(true);
        //define the seed and get the terrain to begin generation
        seed = Random.Range(0, 1000);
        depth = seed / 30f + 5f;
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        Debug.Log(depth);


        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;

        StartCoroutine(PositionShip());


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
        float xCoord = (float)x / width * scale + seed; // add the seed for randomization
        float yCoord = (float)y / height * scale + seed;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }



    IEnumerator PositionShip()
    {
        //Generate random x,y,z position on the terrain
        float randX = Random.Range(xTerrainPos, xTerrainPos + width);
        float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));

        //Apply Offset
        yVal = yVal + 8f;

        //move the ship to the random position
        shipModel.transform.position = new Vector3(randX, yVal, randZ);

        Rigidbody rb = shipModel.AddComponent<Rigidbody>();

        rb.mass = 100000;

        yield return new WaitForSeconds(4f);

        Destroy(rb);

        playerModel.GetComponent<CharacterController>().enabled = false;
        playerModel.transform.position = new Vector3(shipModel.transform.position.x, shipModel.transform.position.y + 3f, shipModel.transform.position.z);
        playerModel.GetComponent<CharacterController>().enabled = true;
        loadingScreen.SetActive(false);
        //(GameObject)Instantiate(prefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
    }
}




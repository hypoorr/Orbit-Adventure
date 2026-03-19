using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{

    public float depth = 20;

    public int width = 800;
    public int height = 800;

    private float xTerrainPos;
    private float zTerrainPos;

    public GameObject prefab;
    public GameObject rockPrefab;
    public GameObject diamondPrefab;
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
        Random.InitState(Mathf.RoundToInt(seed));
        depth = seed / 40f;
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        Debug.Log(depth);


        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;

        StartCoroutine(PositionShip());
        StartCoroutine(SpawnRocks());
        StartCoroutine(SpawnDiamonds());


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
        float[,] heights = new float[width + 1, height + 1];
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
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
        float randX = width / 2f;//Random.Range(xTerrainPos, xTerrainPos + width); //xTerrainPos, xTerrainPos + width);
        float randZ = height / 2f;//Random.Range(zTerrainPos, zTerrainPos + height);//zTerrainPos, zTerrainPos + height);
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(0, 0, 0));

        //Apply Offset
        yVal = yVal + 8f;

        //move the ship to the random position
        shipModel.transform.position = new Vector3(randX, yVal, randZ);

        Rigidbody rb = shipModel.AddComponent<Rigidbody>();

        rb.mass = 100000;
        rb.angularDamping = 2f;  // Angular drag (affects the object's ability to rotate)
        ;
        yield return new WaitForSeconds(4f);

        Destroy(rb);

        playerModel.GetComponent<CharacterController>().enabled = false;
        playerModel.transform.position = new Vector3(shipModel.transform.position.x, shipModel.transform.position.y + 3f, shipModel.transform.position.z);
        playerModel.GetComponent<CharacterController>().enabled = true;
        loadingScreen.SetActive(false);


        //spawn gold block
        randX += Random.Range(-100, 100);
        randZ += Random.Range(-100, 100);
        yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
        yVal += 3f;
        Instantiate(prefab, new Vector3(randX, yVal, randZ), Quaternion.identity);

    }

    IEnumerator SpawnRocks()
    {
        for (int i = 0; i < 200; i++)
        {
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal += 1f;

            Instantiate(rockPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }


    }

    IEnumerator SpawnDiamonds()
    {
        for (int i = 0; i < 100; i++)
        {
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal += 1f;

            Instantiate(diamondPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }


    }
}




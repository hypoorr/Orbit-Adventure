using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;

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
    public GameObject goldPrefab;
    public GameObject enemyPrefab;

    public NavMeshSurface navMeshSurface;
    public PlayerMotor player;

    public float scale = 20f;

    private string[] firstNamePlanet = {"flizzy", "larpy", "gurtified", "perry"};
    private string[] lastNamePlanet = {"type B", "weasle", "flizzable", "octane"};
    public static string planetName;


    public static bool hasEnemies;

    public static List<string> resourcesPresent = new List<string>();

    [SerializeField] private GameObject shipModel;
    [SerializeField] private GameObject playerModel;

    [SerializeField] private GameObject loadingScreen;


    //seed adds randomization to world generation
    public static float seed;


    void Start()
    {
        for (int i = 0; i < TerrainGenerator.resourcesPresent.Count; i++) // destroy previous recorded resources
        {
            resourcesPresent.RemoveAt(i);
        }

        hasEnemies = false;
        loadingScreen.SetActive(true);
        //define the seed and get the terrain to begin generation
        seed = Random.Range(0, 1000);
        Random.InitState(Mathf.RoundToInt(seed));
        depth = seed / 35f;
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        Debug.Log(depth);


        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;

        StartCoroutine(PositionShip());
        StartCoroutine(SpawnRocks());
        StartCoroutine(SpawnGold());
        GeneratePlanetName();



        // RANDOM EVENTS

        if (Random.Range(1, 10) == 1) // 1/10 chance for enemies
        {
            StartCoroutine(SpawnEnemy());
        }
        


        if (Random.Range(1, 5) == 1) // 1/5 chance to spawn diamonds
        {
            StartCoroutine(SpawnDiamonds());
        }

        // 1/5 chance for random gravity
        if (Random.Range(1, 5) == 1)
        {
            player.gravity = Random.Range(-12f, -2f);
            Debug.Log(player.gravity);
        }
        else // default to normal gravity if not randomized
        {
            player.gravity = -9.8f;
        }

        navMeshSurface.BuildNavMesh();



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

    void GeneratePlanetName()
    {
        planetName = firstNamePlanet[Random.Range(0,3)] + " " + lastNamePlanet[Random.Range(0,3)];
        Debug.Log(planetName);
    }



    IEnumerator PositionShip()
    {
        //Generate random x,y,z position on the terrain
        float randX = width / 2f;//Random.Range(xTerrainPos, xTerrainPos + width); //xTerrainPos, xTerrainPos + width);
        float randZ = height / 2f;//Random.Range(zTerrainPos, zTerrainPos + height);//zTerrainPos, zTerrainPos + height);
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(0, 0, 0));

        //Apply Offset
        yVal = yVal + 12f;

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
        //Instantiate(prefab, new Vector3(randX, yVal, randZ), Quaternion.identity);

    }

    IEnumerator SpawnRocks()
    {
        resourcesPresent.Add("Stone");
        for (int i = 0; i < 350; i++)
        {
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal += 0.5f;

            Instantiate(rockPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }


    }

    IEnumerator SpawnDiamonds()
    {
        resourcesPresent.Add("Diamond");
        for (int i = 0; i < 40; i++)
        {
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal += 1f;

            Instantiate(diamondPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }


    }
    IEnumerator SpawnGold()
    {
        resourcesPresent.Add("Gold");
        for (int i = 0; i < 75; i++)
        {
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal += 0.4f;

            Instantiate(goldPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }


    }

    IEnumerator SpawnEnemy()
    {
        hasEnemies = true;
        for (int i = 0; i < 30; i++)
        {
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal += 0.4f;

            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
            newEnemy.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }


    }
}




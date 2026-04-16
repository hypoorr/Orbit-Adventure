using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class TerrainGenerator : MonoBehaviour
{

    public float depth = 20;
    public float scale = 20f;
    public int width = 800;
    public int height = 800;

    private float xTerrainPos;
    private float zTerrainPos;

    public GameObject prefab;
    public GameObject rockPrefab;
    public GameObject diamondPrefab;
    public GameObject goldPrefab;
    public GameObject enemyPrefab;
    public GameObject passiveCreaturePrefab;
    [SerializeField] private GameObject shipModel;
    [SerializeField] private GameObject playerModel;


    public Material floorMaterial;

    public NavMeshSurface navMeshSurface;
    public PlayerMotor player;

    

    private string[] firstNamePlanet = { "flizzy", "larpy", "gurtified", "perry", "deuce" };
    private string[] lastNamePlanet = { "type B", "weasle", "flizzable", "octane", };
    public static string planetName;


    public static bool hasEnemies;

    public static List<string> resourcesPresent = new List<string>();



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
        depth = seed / 25f;

        Terrain terrain = GetComponent<Terrain>();
        floorMaterial = terrain.materialTemplate; // get the terrain material to be able to change colour
        floorMaterial.color = new Color32((byte)Random.Range(0, 100), (byte)Random.Range(0, 100), (byte)Random.Range(0, 100), 1); // assign a random colour to the ground

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        Debug.Log(depth);


        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;

        StartCoroutine(PositionShip());
        StartCoroutine(SpawnRocks());
        StartCoroutine(SpawnGold());
        StartCoroutine(SpawnPassiveCreatures());
        GeneratePlanetName();



        // RANDOM EVENTS

        if (Random.Range(1, 7) == 1) // 1/7 chance for enemies
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

        //bake navmesh in coroutine to avoid freezing the game early on
        StartCoroutine(BakeNavMesh());



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
        planetName = firstNamePlanet[Random.Range(0, 5)] + " " + lastNamePlanet[Random.Range(0, 4)];
        Debug.Log(planetName);
    }

    IEnumerator BakeNavMesh()
    {
        yield return null;
        navMeshSurface.BuildNavMesh();
    }

    IEnumerator PositionShip()
    {
        //Generate random x,y,z position on the terrain
        float randX = width / 2f; //Random.Range(xTerrainPos, xTerrainPos + width); //xTerrainPos, xTerrainPos + width);
        float randZ = height / 2f; //Random.Range(zTerrainPos, zTerrainPos + height);//zTerrainPos, zTerrainPos + height);
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
        playerModel.transform.position = new Vector3(shipModel.transform.position.x, shipModel.transform.position.y + 3f, shipModel.transform.position.z); //teleport the player to the ship
        playerModel.GetComponent<CharacterController>().enabled = true;
        loadingScreen.SetActive(false);

    }

    IEnumerator SpawnRocks()
    {
        resourcesPresent.Add("Stone");
        for (int i = 0; i < 350; i++)
        {
            //pick a random X and Z position for the resource
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ)); //find the Y pos on the terrain of the X and Z position
            yVal += 0.5f; //add a small offset to avoid being in the ground

            Instantiate(rockPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity); //create the prefab of the material at the coordinates
            yield return new WaitForSeconds(0.01f);
        }


    }

    IEnumerator SpawnDiamonds()
    {
        resourcesPresent.Add("Diamond");
        for (int i = 0; i < 40; i++)
        {
            //pick a random X and Z position for the resource
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ)); //find the Y pos on the terrain of the X and Z position
            yVal += 0f; //add a small offset to avoid being in the ground

            Instantiate(diamondPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity); //create the prefab of the material at the coordinates
            yield return new WaitForSeconds(0.01f);
        }


    }
    IEnumerator SpawnGold()
    {
        resourcesPresent.Add("Gold");
        for (int i = 0; i < 75; i++)
        {
            //pick a random X and Z position for the resource
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ)); //find the Y pos on the terrain of the X and Z position
            yVal += 0.4f; //add a small offset to avoid being in the ground

            Instantiate(goldPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity); //create the prefab of the material at the coordinates
            yield return new WaitForSeconds(0.01f);
        }


    }

    IEnumerator SpawnEnemy()
    {
        hasEnemies = true;
        for (int i = 0; i < 30; i++)
        {
            //pick a random X and Z position for the NPC
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ)); //find the Y pos on the terrain of the X and Z position
            yVal += 0.4f; //add a small offset to avoid being in the ground

            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(randX, yVal, randZ), Quaternion.identity); //create the prefab of the NPC at the coordinates
            newEnemy.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }


    }

    IEnumerator SpawnPassiveCreatures()
    {
        for (int i = 0; i < 30; i++)
        {
            //pick a random X and Z position for the NPC
            float randX = Random.Range(xTerrainPos, xTerrainPos + width);
            float randZ = Random.Range(zTerrainPos, zTerrainPos + height);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ)); //find the Y pos on the terrain of the X and Z position
            yVal += 0.4f; //add a small offset to avoid being in the ground

            GameObject newCreature = Instantiate(passiveCreaturePrefab, new Vector3(randX, yVal, randZ), Quaternion.identity); //create the prefab of the NPC at the coordinates
            newCreature.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }


    }
}




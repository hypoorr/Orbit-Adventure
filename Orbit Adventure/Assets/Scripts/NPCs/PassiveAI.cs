using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PassiveAI : MonoBehaviour
{
    //pick a place nearby the creature and move to it every 10-15 seconds
    public Terrain terrain;

    void Start()
    {
        StartCoroutine(moveCreature());
    }
    
    IEnumerator moveCreature()
    {
        while (true)
        {
        yield return new WaitForSeconds(Random.Range(15, 25));
        float randomX = gameObject.transform.position.x +- 25f;
        float randomZ = gameObject.transform.position.z +- 25f;
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));
        gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(randomX, yVal, randomZ));
        }

    }
}

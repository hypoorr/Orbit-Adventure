using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MenuObjectSpawn : MonoBehaviour
{
    public GameObject ballRef;

    void SpawnBall()
    {
        GameObject spawnedBall = Instantiate(ballRef, transform);
        spawnedBall.transform.position = new Vector3(Random.Range(-30, 30), 25, Random.Range(-30, 30));
        spawnedBall.SetActive(true);
        StartCoroutine(DestroyBall(spawnedBall));
        StartCoroutine(CheckBall(spawnedBall));

    }
    IEnumerator DestroyBall(GameObject ball)
    {
        yield return new WaitForSeconds(120f);
        Destroy(ball);
    }
    IEnumerator CheckBall(GameObject ball)
    {
        while (ball)
        {
            if (ball.transform.position.y < -50) // despawn offscreen
            {
                Destroy(ball);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Update()
    {
        SpawnBall();
    }
}
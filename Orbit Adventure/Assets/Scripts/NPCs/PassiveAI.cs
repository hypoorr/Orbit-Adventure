using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PassiveAI : MonoBehaviour
{
    //pick a place nearby the creature and move to it every 10-15 seconds
    public Terrain terrain;
    private Animator animator;
    private NavMeshAgent agent;
    private AnimatorClipInfo[] currentAnimation;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(moveCreature());
    }
    void Update()
    {
        currentAnimation = animator.GetCurrentAnimatorClipInfo(0);
        if (agent.hasPath)
        {
            if (currentAnimation[0].clip.name != "Walking")
            {
                UpdateAnimation("Walking");
            }

        }
        else
        {
            if (currentAnimation[0].clip.name != "Idle")
            {
                UpdateAnimation("Idle");
            }
        }
    }

    void UpdateAnimation(string animation)
    {
        animator.CrossFadeInFixedTime(animation, 0.2f);
    }
    IEnumerator moveCreature()
    {
        while (true)
        {
        yield return new WaitForSeconds(Random.Range(15, 25));
        float randomX = gameObject.transform.position.x + Random.Range(-25f, 25f); 
        float randomZ = gameObject.transform.position.z + Random.Range(-25f, 25f);
        float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ)); // find the Y value on the terrain
        agent.SetDestination(new Vector3(randomX, yVal, randomZ)); // set agent destination
        }

    }
}

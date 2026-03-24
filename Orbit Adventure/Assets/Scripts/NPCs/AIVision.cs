using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public GameObject shipModel;
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private Rigidbody rb;

    private bool isCaught;

    public UnityEvent OnPlayerCatch;

    Animator animator;

    


    public NavMeshAgent agent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //playerRef = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(FOVRoutine());



    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == playerRef)
        {
            if (!isCaught)
            {
                isCaught = true;
                Debug.Log("Player caught!");
                OnPlayerCatch.Invoke();
            }

        }
    }

    public void SendToShip()
    {
        playerRef.GetComponent<CharacterController>().enabled = false;
        playerRef.transform.position = new Vector3(shipModel.transform.position.x, shipModel.transform.position.y + 3f, shipModel.transform.position.z);
        playerRef.GetComponent<CharacterController>().enabled = true;
        agent.Stop();
    }
    
    public Transform target;

    
    public float detectionRadius = 5f;

    
    public bool useSquaredDistance = true;

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned");
            return;
        }

        bool isNear = false;

        if (useSquaredDistance)
        {
            float sqrDistance = (target.position - transform.position).sqrMagnitude;
            float sqrRadius = detectionRadius * detectionRadius;
            isNear = sqrDistance <= sqrRadius;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.position);
            isNear = distance <= detectionRadius;
        }

        if (isNear)
        {
            //Debug.Log($"{name} is within {detectionRadius} units of {target.name}");
            //Attack();
        }
        //SetAnimations();
        if (canSeePlayer) // if can see the player, run at them
        {

            Vector3 playerPos = playerRef.transform.position;
            agent.SetDestination(playerPos);
        }
        
    }


    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }


    // ---------- //
    // ANIMATIONS //
    // ---------- //

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) // starts the animation corresponding to the current action
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }
    void SetAnimations() // checks if the player is idle or walking
    {
        // If ai is not attacking
        if (!attacking)
        {
            if (!canSeePlayer)
            { ChangeAnimationState(IDLE); }
            else
            { ChangeAnimationState(WALK); }
        }
    }


    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 7;

    //public GameObject hitEffect;
    //public AudioClip swordSwing;
    //public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack() // when enemy attacks, sword swings and animations play
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        //Invoke(nameof(AttackRaycast), attackDelay);

        //audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            
            attackCount = 0;
        }


    }

    

    void ResetAttack() // resets attack
    {
        attacking = false;
        readyToAttack = true;
    }


}
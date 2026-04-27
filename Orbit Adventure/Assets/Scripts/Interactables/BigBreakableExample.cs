using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BigBreakableExample : Breakable
{

    [SerializeField] private string resourceToGrant;

    [SerializeField] private AudioClip[] hitSounds;
    private AudioSource audioSource;
    private Inventory inventory;
    private int hp;
    private Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
        inventory = FindFirstObjectByType<Inventory>();
        audioSource = GetComponent<AudioSource>();
        hitSounds = Resources.LoadAll<AudioClip>("HitSounds"); // load hit sounds
        hp = 150;

    }


    void PlayRandomSound()
    {
        int index = UnityEngine.Random.Range(0, hitSounds.Length);
        audioSource.PlayOneShot(hitSounds[index]);
    }
    IEnumerator BreakEffect()
    {

        StartCoroutine(SpawnParticle());

        transform.localScale = scale; // reset scale

        transform.DOScale(scale / 1.2f, 0.1f) //creates a scale using an easing
            .SetEase(Ease.OutQuart);

        yield return new WaitForSeconds(0.1f); //Waits for first animation to complete

        transform.DOScale(scale, 0.4f) // scales back up
            .SetEase(Ease.OutQuart);

    }

    private List<GameObject> activeParticles = new List<GameObject>();
    IEnumerator SpawnParticle()
    {

        //spawn corresponding particle for breakable
        switch (resourceToGrant)
        {
            case "Stone":
                GameObject stoneParticle = Instantiate(GameObject.FindWithTag("StoneParticle"), gameObject.transform.position, Quaternion.identity, gameObject.transform);
                activeParticles.Add(stoneParticle);
                yield return new WaitForSeconds(0.5f);
                stoneParticle.SetActive(false);
                Destroy(stoneParticle);
                if (stoneParticle)
                {
                    Destroy(stoneParticle);
                }
                break;
            case "Gold":
                GameObject goldParticle = Instantiate(GameObject.FindWithTag("GoldParticle"), gameObject.transform.position, Quaternion.identity, gameObject.transform); // parents to ore to delete properly
                activeParticles.Add(goldParticle);
                yield return new WaitForSeconds(0.5f);
                goldParticle.SetActive(false);
                Destroy(goldParticle);
                if (goldParticle)
                {
                    Destroy(goldParticle);
                }
                break;
            case "Diamond":
                GameObject diamondParticle = Instantiate(GameObject.FindWithTag("DiamondParticle"), gameObject.transform.position, Quaternion.identity, gameObject.transform);
                activeParticles.Add(diamondParticle);
                yield return new WaitForSeconds(0.5f);
                diamondParticle.SetActive(false);
                Destroy(diamondParticle);
                if (diamondParticle)
                {
                    Destroy(diamondParticle);
                }
                break;
        }


    }

    protected override void Interact()
    {
        if (hp >= 0)
        {
            hp -= 25;
            PlayRandomSound();
            StartCoroutine(BreakEffect());
        }
        else
        {
            inventory.AddItem(resourceToGrant, UnityEngine.Random.Range(7,12), false);
            Debug.Log("Interacted with " + gameObject.name);
            Destroy(gameObject);
        }

    }
}

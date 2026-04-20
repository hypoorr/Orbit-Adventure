using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BreakableExample : Breakable
{

    [SerializeField] private string resourceToGrant;
    private Inventory inventory;
    private int hp;
    private Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
        inventory = FindFirstObjectByType<Inventory>();
        hp = 50;

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

    IEnumerator SpawnParticle()
    {
        //spawn corresponding particle for breakable
        switch (resourceToGrant)
        {
            case "Stone":
                GameObject stoneParticle = Instantiate(GameObject.FindWithTag("StoneParticle"), gameObject.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                stoneParticle.SetActive(false);
                Destroy(stoneParticle);
                break;
            case "Gold":
                GameObject goldParticle = Instantiate(GameObject.FindWithTag("GoldParticle"), gameObject.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                goldParticle.SetActive(false);
                Destroy(goldParticle);
                break;
            case "Diamond":
                GameObject diamondParticle = Instantiate(GameObject.FindWithTag("DiamondParticle"), gameObject.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                diamondParticle.SetActive(false);
                Destroy(diamondParticle);
                break;
        }
    }

    protected override void Interact()
    {
        if (hp >= 0)
        {
            hp -= 25;
            StartCoroutine(BreakEffect());
        }
        else
        {
            inventory.AddItem(resourceToGrant, 1, false);
            SpawnParticle();
            Debug.Log("Interacted with " + gameObject.name);
            Destroy(gameObject);
        }

    }
}

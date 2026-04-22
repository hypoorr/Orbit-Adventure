using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class StickyNote : Interactable
{
    public RawImage image;

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        image.enabled = true;
        StartCoroutine(HideImage());
    }

    IEnumerator HideImage()
    {
        yield return new WaitForSeconds(5f);
        image.enabled = false;
    }
}

using NUnit.Framework.Interfaces;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField] private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;       
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerUI)
        {
            playerUI.UpdateText(string.Empty);
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward); // create ray cast
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null) // check if ray hits an interactable object
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage); // update interact prompt text
                if (inputManager.onFoot.Interact.triggered) // if interact is pressed, run interaction script
                {
                    interactable.BaseInteract();
                }
            }

            if (hitInfo.collider.GetComponent<Breakable>() != null) // check if ray hits a breakable object
            {
                Breakable breakable = hitInfo.collider.GetComponent<Breakable>();
                playerUI.UpdateText(breakable.promptMessage); // update interact prompt text
                if (inputManager.onFoot.Break.triggered) // if interact is pressed, run breakable script
                {
                    breakable.BaseInteract();
                }
            }            
        }

    }
}


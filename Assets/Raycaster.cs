using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycaster : MonoBehaviour
{
    Camera playerCamera;
    public float raycastLen = 10f;
    public GameObject currentObject;

    Interactable interactable;
    public bool isOverUI;

    XRCardboardInputModule inputModule;
    void Start()
    {
        playerCamera = transform.GetComponentInChildren<Camera>();
        inputModule = this.GetComponentInChildren<XRCardboardInputModule>();
    }

    // Update is called once per frame
    void Update()
    {
        isOverUI = IsPointerOverUIObject();

        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (!isOverUI)
        {
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, raycastLen))
            {
                if (hit.collider.gameObject != currentObject && currentObject && interactable)
                {
                    interactable.isHover = false;
                   
                }

                currentObject = hit.collider.gameObject;

                if (currentObject.GetComponent<Interactable>())
                {
                    interactable = currentObject.GetComponent<Interactable>();
                    interactable.isHover = true;
                }


            }
            else
            {
               
                if (interactable.gameObject)
                {
                    interactable.isHover = false;
                }
                currentObject = null;

            }
        }
        else
        {
           
            if (interactable.gameObject)
            {
                interactable.isHover = false;
            }
            currentObject = null;
        }
    }

    private bool IsPointerOverUIObject()
    {
        if (inputModule.currentTarget)
        {
            if(inputModule.currentTarget.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

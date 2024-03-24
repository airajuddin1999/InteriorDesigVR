using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Interactable))]

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Rigidbody))]

public class SofaInteraction : MonoBehaviour
{
    public GameObject canvasToDisplay;

    public bool isSelected = false;

    public GameObject Player;

    public float TimeToDeselect;

    public Color[] colors;

    public AudioClip changeSound;

    public GameObject[] objectsToHide;

    public float distance = 2f;

    public Vector3 rotationAxis = new Vector3(0, 0, 0);

    public bool isSelectedToMove;


    float currentTime = 0.0f;
    Ray myRay;      // initializing the ray
    RaycastHit hit; // initializing the raycasthit

    //References
    Interactable interactable;
    Outline outline;
    MeshRenderer meshRenderer;
    Material mat;
    Rigidbody rb;


    void Start()
    {
        outline = this.GetComponent<Outline>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        interactable = this.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.isHover)
        {
            if (Input.GetKeyDown(KeyCode.X) && !isSelected)
            {
                SelectSofa(true);
                //ChangeTexture();
            }
        }

        if (canvasToDisplay.activeInHierarchy)
        {
            canvasToDisplay.transform.LookAt(Player.transform.position);
            canvasToDisplay.transform.localEulerAngles = new Vector3(0, canvasToDisplay.transform.localEulerAngles.y, 0.0f);
        }

        if (outline) outline.enabled = interactable.isHover;

        if (!interactable.isHover && canvasToDisplay.activeInHierarchy)
        {
            currentTime += Time.deltaTime;
            if (currentTime > TimeToDeselect)
            {
                SelectSofa(false);
                currentTime = 0.0f;
            }
        }
    }

    void SelectSofa(bool value)
    {
        isSelected = value;


        canvasToDisplay.SetActive(value);
        myRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        canvasToDisplay.transform.position = myRay.GetPoint(distance);


        outline.OutlineColor = (value) ? Color.blue : Color.white;

        if(objectsToHide.Length > 0)
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(!value);
        }
    }


    /// <summary>
    /// This function is used to change the colors of wall.
    /// </summary>
    /// <param name="id"></param>
    public void ChangeColor(int id)
    {
        mat.color = colors[id - 1];
        if (changeSound) AudioSource.PlayClipAtPoint(changeSound, this.transform.position);
    }


    // This function will be used to Rotate The Object when holding the button
    public void Rotate()
    {
        Vector3 rotation = rotationAxis;
        rotation *= Time.deltaTime;

        transform.Rotate(rotation);
    }

    // This functiion will be used to Delete The Object
    public void Delete()
    {
        Destroy(gameObject);
    }
}

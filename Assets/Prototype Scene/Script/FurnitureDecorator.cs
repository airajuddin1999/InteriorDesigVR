using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class FurnitureDecorator : MonoBehaviour
{
    public GameObject currentSelected;
    public GameObject currentInHand;
    public TMP_Text currentSelectedText;

    public GameObject[] beds,sofas,tables,chairs;
    public bool groundObject = true;
    public string groundTag = "Ground";
    public string wallTag = "Wall";

    public KeyCode actionKey = KeyCode.X;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSelected)
        {
             if(CheckRay() && (Input.GetButtonDown("Fire3") || Input.GetKeyDown(actionKey)))
            {
                GameObject instantiatedGO = GameObject.Instantiate(currentSelected, hit.point, Quaternion.identity);
                instantiatedGO.name = currentSelected.name;
                currentSelected = null;
                currentSelectedText.text = "";

            }
            
        }
    }


    bool CheckRay()
    {
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);

        // Create a ray from the camera through the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);

        // Create a RaycastHit variable to store information about what the ray hits
       

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            if (groundObject)
            {
                if (hit.transform.tag == groundTag)
                {

                    Debug.Log("Hit object: " + hit.transform.name);
                    return true;
                }
                else
                {

                    return false;
                }
            }
            else
            {
                if (hit.transform.tag == wallTag)
                {

                    Debug.Log("Hit object: " + hit.transform.name);
                    return true;
                }
                else
                {

                    return false;
                }
            }
               

        }
        else
        {
            
            return false;

        }

    }



    public void SelectBed(int id)
    {
      currentSelected = beds[id];
        currentSelectedText.fontSize = 30f;
      currentSelectedText.text = "Bed " + id+1 + " is selected. \n Press X by focusing on ground to Deploy";
      groundObject = true;
      return;
       
    }

    public void SelectSofa(int id)
    {
        currentSelected = sofas[id];
        currentSelectedText.fontSize = 30f;
        currentSelectedText.text = "Sofa " + id + 1 + " is selected. \n Press X by focusing on ground to Deploy";
        groundObject = true;
        return;

    }

    public void SelectTable(int id)
    {
        currentSelected = tables[id];
        currentSelectedText.fontSize = 30f;
        currentSelectedText.text = "Table " + id + 1 + " is selected. \n Press X by focusing on ground to Deploy";
        groundObject = true;
        return;

    }

    public void SelectChairs(int id)
    {
        currentSelected = chairs[id];
        currentSelectedText.fontSize = 30f;
        currentSelectedText.text = "Chair " + id + 1 + " is selected. \n Press X by focusing on ground to Deploy";
        groundObject = true;
        return;

    }


}

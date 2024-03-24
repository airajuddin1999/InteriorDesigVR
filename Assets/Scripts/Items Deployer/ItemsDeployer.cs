using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsDeployer : MonoBehaviour
{
    public string currentArea;
    public GameObject currentSelected;
    public string neededTag;

    public float distance = 2f;
    [Header("Canvases")]
    public GameObject DrawingRoomDeployer;

    public GameObject[] Sofas;

    Ray myRay;      // initializing the ray
    RaycastHit hit; // initializing the raycasthit

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            if(currentArea == "DrawingRoom")
            {
                DrawingRoomDeployer.SetActive(true);
                myRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                DrawingRoomDeployer.transform.position = myRay.GetPoint(distance);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (currentSelected && CheckRay())
            {
                GameObject instantiatedGO = GameObject.Instantiate(currentSelected, hit.point, Quaternion.identity);
                instantiatedGO.name = currentSelected.name;
                currentSelected = null;
                
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
            if (neededTag == "Wall")
            {
                if (hit.transform.tag == "Wall")
                {

                    Debug.Log("Hit object: " + hit.transform.name);
                    return true;
                }
                else
                {

                    return false;
                }
            }
            else if(neededTag == "Floor")
            {
                if (hit.transform.tag == "Floor")
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
                return false;
            }


        }
        else
        {

            return false;

        }

    }

    public void ChangeCurrentArea(string area)
    {
        currentArea = area;
    }


    public void SelectSofa(int id)
    {
        currentSelected = Sofas[id - 1];
        neededTag = "Floor";
    }


    
}

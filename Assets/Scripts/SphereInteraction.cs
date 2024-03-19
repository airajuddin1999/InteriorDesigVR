using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckRay() && (Input.GetButton("X") || Input.GetButton("Fire1")))
        {
            Player.gameObject.SetActive(false);
            Player.transform.position = this.transform.position;
            Player.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }


    bool CheckRay()
    {
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);

        // Create a ray from the camera through the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);

        // Create a RaycastHit variable to store information about what the ray hits
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == this.transform)
            {
                this.GetComponent<Outline>().enabled = true;
                Debug.Log("Hit object: " + hit.transform.name);
                return true;
            }
            else
            {
                this.GetComponent<Outline>().enabled = false;
                return false;
            }

        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
            return false;

        }

    }
}

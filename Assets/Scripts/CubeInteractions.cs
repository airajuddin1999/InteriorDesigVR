using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteractions : MonoBehaviour
{
    public bool rotate = false;
    public float rotationSpeed = 2f;
    public bool translation;
    public float translationSpeed = 2f;

    public bool changeColor;
    public Color originalColor;
    public Color newColor;

    MeshRenderer mesh;

    void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        originalColor = mesh.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRay();
        if (rotate && CheckRay() && (Input.GetButton("X")|| Input.GetButton("Fire1"))) transform.Rotate(0, rotationSpeed, 0);

        if (translation && CheckRay() && (Input.GetButton("X") || Input.GetButton("Fire1"))) transform.Translate(translationSpeed, 0, 0);

        if (changeColor && CheckRay() && (Input.GetButton("X") || Input.GetButton("Fire1"))) mesh.material.color = newColor;
        else
            
            mesh.material.color = originalColor;

       

        

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


    public void Translation(bool value)
    {
        translation = value;
    }

    public void Rotation(bool value)
    {
        rotate = value;
    }

    public void ColorChanging(bool value)
    {
        changeColor = value;
    }
}

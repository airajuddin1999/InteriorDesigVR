using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WallsDecorator : MonoBehaviour
{
    [System.Serializable]
    public class OptionAvailble
    {
        public Texture tex;
        public Vector2 tiling;
    }

    public Color[] colorsArray;

    [SerializeField]
    public OptionAvailble[] textureDS;
    public Button b1;

     Material currentMat;
    void Start()
    {

        currentMat = this.GetComponent<MeshRenderer>().material;

       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ChangeTexTo(int texNum)
    {
        currentMat.mainTexture = textureDS[texNum].tex;
        currentMat.mainTextureScale = textureDS[texNum].tiling;
    }

    public void ChangeColorTo(int num )
    {
        currentMat.color = colorsArray[num];
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
               // this.GetComponent<Outline>().enabled = true;
                Debug.Log("Hit object: " + hit.transform.name);
                return true;
            }
            else
            {
              //  this.GetComponent<Outline>().enabled = false;
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

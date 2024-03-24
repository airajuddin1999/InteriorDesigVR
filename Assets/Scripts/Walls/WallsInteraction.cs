using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Interactable))]
public class WallsInteraction : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{

    public bool focused;
    public bool isSelected = false;

    [System.Serializable]
    public class DifferentTextures
    {
        public Texture tex;
        public Vector2 tiling;
        public Vector2 offSet;
    }
    [SerializeField] public DifferentTextures[] textures;
    [SerializeField] public Color[] colors;

    public int currentTex = 0;
    public int currentColor = 0;

    public AudioClip changeSound;
   
    public GameObject canvasToDisplay;
    public GameObject Player;


    [Header("Enable/Disable Items")]
    public GameObject[] objectsToHide;

    //References
    Outline outline;
    MeshRenderer meshRenderer;
    Material mat;

    public float distance = 2f;


    public float TimeToDeselect = 5f;
    float currentTime = 0.0f;
   

    Ray myRay;      // initializing the ray
    RaycastHit hit; // initializing the raycasthit
    Interactable interactable;



    void Start()
    {
        outline = this.GetComponent<Outline>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        interactable = this.GetComponent<Interactable>();

        //objectsToHide = this.transform.GetComponentsInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.isHover)
        {
            if (Input.GetKeyDown(KeyCode.X) && !isSelected)
            {
                SelectWall(true);
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
            currentTime+= Time.deltaTime;
            if(currentTime > TimeToDeselect)
            {
                SelectWall(false);
                currentTime = 0.0f;
            }
        } 
    }


    /// <summary>
    /// When Wall is selected canvas will start displaying.
    /// Not important objects will be hided to give clear focus on selected object.
    /// </summary>
    /// <param name="value"></param>
    public void SelectWall(bool value)
    {
        isSelected = value;

        
            canvasToDisplay.SetActive(value);
            myRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            canvasToDisplay.transform.position = myRay.GetPoint(distance);
        

        outline.OutlineColor = (value) ?Color.blue : Color.white;

        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(!value);
        }
    }


    //This Function will change the texture of the walls according to the given id and play sound when texture is changed
    public void ChangeTexture(int id)
    {
        currentTex = id - 1;
        if (currentTex > textures.Length - 1) currentTex = 0;
        mat.mainTexture = textures[currentTex].tex;
        mat.mainTextureScale = textures[currentTex].tiling;
        if (changeSound) AudioSource.PlayClipAtPoint(changeSound, this.transform.position);
    }

    /// <summary>
    /// This fuction will remove the texture 
    /// </summary>
    public void NoTexture()
    {
        mat.mainTexture = null;
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

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline))]
public class WallsInteraction : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
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
   
    public GameObject[] canvasToDisplay;
    public GameObject Player;


    [Header("Enable/Disable Items")]
    public GameObject[] objectsToHide;

    //References
    Outline outline;
    MeshRenderer meshRenderer;
    Material mat;



    void Start()
    {
        outline = this.GetComponent<Outline>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (focused)
        {
            if (Input.GetKeyDown(KeyCode.X) && !isSelected)
            {
                SelectWall(true);
                //ChangeTexture();
            }
        }

        foreach (GameObject canvas in canvasToDisplay)
        {
            if (canvas.activeInHierarchy)
            {
                canvas.transform.LookAt(Player.transform.position);
                canvas.transform.localEulerAngles = new Vector3(0, canvas.transform.localEulerAngles.y, 0.0f);
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

        foreach (GameObject canvas in canvasToDisplay)
        {
            canvas.SetActive(value);
        }

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

    public void SetFocus(bool value)
    {
        focused = value;

        if (outline) outline.enabled = value;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        SetFocus(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetFocus(false);
        SelectWall(false);
    }


}

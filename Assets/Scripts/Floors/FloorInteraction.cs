using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Interactable))]
public class FloorInteraction : MonoBehaviour 
{
    [System.Serializable]
    public class DifferentTextures
    {
        public Texture tex;
        public Vector2 tiling;
        public Vector2 offSet;
    }
    public bool focused;

    [SerializeField]
    public DifferentTextures[] textures;
    public int currentTex = 0;

    public AudioClip changeSound;
    //References
    Outline outline;
    MeshRenderer meshRenderer;
    Material mat;
    Interactable interactable;


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
        if(interactable.isHover)
        {
            if(Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("jsX"))
            {
                ChangeTexture();
            }
        }

        if (outline) outline.enabled = interactable.isHover;
    }


    public void ChangeTexture()
    {
        currentTex++;
        if (currentTex > textures.Length-1) currentTex = 0;
        mat.mainTexture = textures[currentTex].tex;
        mat.mainTextureScale = textures[currentTex].tiling;
        mat.color = Color.white;
        if (changeSound) AudioSource.PlayClipAtPoint(changeSound, this.transform.position);
    }



}

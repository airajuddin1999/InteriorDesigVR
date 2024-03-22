using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Outline))]
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


    void Start()
    {
        outline = this.GetComponent<Outline>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(focused)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                ChangeTexture();
            }
        }
    }


    public void ChangeTexture()
    {
        currentTex++;
        if (currentTex > textures.Length-1) currentTex = 0;
        mat.mainTexture = textures[currentTex].tex;
        mat.mainTextureScale = textures[currentTex].tiling;
        if (changeSound) AudioSource.PlayClipAtPoint(changeSound, this.transform.position);
    }


    public void SetFocus(bool value)
    {
        focused = value;

        if (outline) outline.enabled = value;
    }



}

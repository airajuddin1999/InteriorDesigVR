using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Outline))]
public class TVInteraction : MonoBehaviour
{
    public bool isOn = false;
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public GameObject image;
    public GameObject Player;

    Interactable interactable;
    Outline outline;
    void Start()
    {
        interactable = this.GetComponent<Interactable>();
        outline = this.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.isHover)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isOn = !isOn;
                if (isOn)
                    videoPlayer.Play();
                else
                    videoPlayer.Stop();

                image.SetActive(isOn);
            }

            if (canvas)
            {
                canvas.SetActive(true);
                canvas.transform.LookAt(Player.transform.position);
                canvas.transform.localEulerAngles = new Vector3(canvas.transform.localEulerAngles.x, canvas.transform.localEulerAngles.y, 0.0f);
            }
        }
        else
        {
            if (canvas)
            {
                canvas.SetActive(false);
            }
        }

        outline.enabled = interactable.isHover;

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Outline))]
public class LightInteractions : MonoBehaviour
{
    public GameObject LightObject;
    public bool lightOn = true;
    public GameObject Player;

    [Header("Canvas Objects")]
    public GameObject canvas;

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
            if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("jsX"))
            {
                lightOn = !lightOn;
                LightObject.SetActive(lightOn);
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
            if(canvas)
            {
                canvas.SetActive(false);
            }
        }

        outline.enabled = interactable.isHover;

    }


}

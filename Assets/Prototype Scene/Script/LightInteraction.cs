using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LightInteraction : MonoBehaviour
{
    public bool isHover;
    public KeyCode actionKey = KeyCode.X;

    Outline outline;

    public Material emmisiveMaterail;

    public UnityEvent actionEvent;
    public bool lightOn = false;
    public GameObject light;

    void Start()
    {
        outline = this.GetComponent<Outline>();
        outline.enabled = false;

        outline.OutlineColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {

        if (isHover && Input.GetKeyDown(actionKey))
        {
            lightOn = !lightOn;
            actionEvent.Invoke();
        }

        if (isHover && Input.GetButtonDown("Fire3"))
        {
            actionEvent.Invoke();
        }

        if (lightOn)
        {
            if (emmisiveMaterail)
                emmisiveMaterail.EnableKeyword("_EMISSION");

            light.SetActive(true);

        }
        else
        {
            light.SetActive(false);
            if (emmisiveMaterail)
            emmisiveMaterail.DisableKeyword("_EMISSION");
        }


    }


    public void ChangeHoverState(bool value)
    {
        isHover = value;
        if (value)
            outline.enabled = true;
        else
            outline.enabled = false;
    }
}

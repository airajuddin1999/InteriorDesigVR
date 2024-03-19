using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    public bool isHover;
    public KeyCode actionKey = KeyCode.X;

    public UnityEvent actionEvent;
    AudioClip clip;
    void Start()
    {
        clip = Resources.Load<AudioClip>("ButtonClickSound");
    }

    // Update is called once per frame
    void Update()
    {
        if(isHover && Input.GetKeyDown(actionKey))
        {
            AudioSource.PlayClipAtPoint(clip, this.transform.position);
            actionEvent.Invoke();
        }

        if(isHover && Input.GetButtonDown("Fire3"))
        {
            AudioSource.PlayClipAtPoint(clip, this.transform.position);
            actionEvent.Invoke();
        }
    }

    public void ChangeHoverState(bool value)
    {
        isHover = value;
    }
}

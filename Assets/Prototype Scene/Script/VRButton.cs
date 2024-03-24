using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VRButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public bool isHover;
    public KeyCode actionKey = KeyCode.X;
    public bool isHold;

    public UnityEvent actionEvent;
    AudioClip clip;
    void Start()
    {
        clip = Resources.Load<AudioClip>("ButtonClickSound");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHold)
        {
            if (isHover && Input.GetKeyDown(actionKey))
            {
                AudioSource.PlayClipAtPoint(clip, this.transform.position);
                actionEvent.Invoke();
            }


            if (isHover && Input.GetButtonDown("Fire3"))
            {
                AudioSource.PlayClipAtPoint(clip, this.transform.position);
                actionEvent.Invoke();
            }
        }

        else
        {
            if (isHover && Input.GetKey(actionKey))
            {
              
                actionEvent.Invoke();
            }


            if (isHover && Input.GetButton("Fire3"))
            {
               
                actionEvent.Invoke();
            }
        }
    }

    public void ChangeHoverState(bool value)
    {
        isHover = value;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeHoverState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeHoverState(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Interactable))]
public class WindowsInteraction : MonoBehaviour
{
    public GameObject WindowFrameLeft;
    public GameObject WindowFrameRight;
    public AudioClip windowInteractionSound;
    public GameObject canvas;
    public GameObject Player;
    

    public bool isOpen;

    Outline outline;
    Interactable interactable;

    public bool IsAnim = false;

    Animator leftFrameAnim;
    Animator rightFrameAnim;

    void Start()
    {
        outline = this.GetComponent<Outline>();
        interactable = this.GetComponent<Interactable>();
        leftFrameAnim = WindowFrameLeft.GetComponent<Animator>();
        rightFrameAnim = WindowFrameRight.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.isHover)
        {
            if (canvas)
            {

                canvas.SetActive(true);
                canvas.transform.LookAt(Player.transform.position);
                canvas.transform.localEulerAngles = new Vector3(canvas.transform.localEulerAngles.x, canvas.transform.localEulerAngles.y, 0.0f);
            }

           
            
                if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("jsX"))
                {
                    rightFrameAnim.SetBool("isOpen", !isOpen);
                    leftFrameAnim.SetBool("isOpen", !isOpen);
                    isOpen = !isOpen;
                    AudioSource.PlayClipAtPoint(windowInteractionSound, this.transform.position);
                    if(isOpen)
                    Invoke("CloseDoor", 4);
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

    public void CloseDoor()
    {
        leftFrameAnim.SetBool("isOpen", !isOpen);
        rightFrameAnim.SetBool("isOpen", !isOpen);
        AudioSource.PlayClipAtPoint(windowInteractionSound, this.transform.position);
        isOpen = false;

    }


}

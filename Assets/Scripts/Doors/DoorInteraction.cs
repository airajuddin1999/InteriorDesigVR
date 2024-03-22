using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public bool focused = false;
    public bool isOpen = false;
    public bool canOpened = false;

    [Header("Animation Stuff")]
    public Animator animator;

    [Header("UI Stuff")]
    public GameObject UI_Text1;
    public GameObject UI_Text2;

    Outline outline;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    void Start()
    {
        outline = this.GetComponent<Outline>();
        animator = this.GetComponent<Animator>();

        if (outline) outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpened && focused)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                animator.SetBool("OpenDoor", !isOpen);
                isOpen = true;
                AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
                Invoke("CloseDoor", 4);
            }
        }
    }


    public void CloseDoor()
    {
        animator.SetBool("OpenDoor", !isOpen);
        AudioSource.PlayClipAtPoint(doorCloseSound, this.transform.position);
        isOpen = false;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isOpen && focused)
        {
            canOpened = true;
            UI_Text1.SetActive(true);
            UI_Text2.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canOpened = false;
            UI_Text1.SetActive(false);
            UI_Text2.SetActive(false);
        }
    }

    public void SetFocus(bool value)
    {
        focused = value;

        if (outline) outline.enabled = value;
    }
}

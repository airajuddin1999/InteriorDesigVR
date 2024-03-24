using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Outline))]
public class KitchenBenchAnim : MonoBehaviour
{
    public Animator mainAnimator;
    public string param;

    public bool isOpen = false;

    public GameObject canvas;

    public AudioClip interactionSound;

    Interactable interactable;
    Outline outline;

    public GameObject Player;
    void Start()
    {
        interactable = this.GetComponent<Interactable>();
        outline = this.GetComponent<Outline>();
        if (!Player) Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (interactable.isHover)
        {
            if (canvas)
            {

                canvas.SetActive(true);
                canvas.transform.LookAt(Player.transform.position);
                canvas.transform.localEulerAngles = new Vector3(canvas.transform.localEulerAngles.x, canvas.transform.localEulerAngles.y, 0.0f);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    isOpen = !isOpen;
                    mainAnimator.SetBool(param, isOpen);
                    
                    AudioSource.PlayClipAtPoint(interactionSound, this.transform.position);
                    if (isOpen)
                        Invoke("CloseItem", 4);
                }
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


    public void CloseItem()
    {
        mainAnimator.SetBool(param, isOpen);
        AudioSource.PlayClipAtPoint(interactionSound, this.transform.position);
        isOpen = false;

    }
}

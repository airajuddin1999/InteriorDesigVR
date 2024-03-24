using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG;
using DG.Tweening;

public class FurnitureInteraction : MonoBehaviour
{
    public bool isHover;
    public KeyCode actionKey = KeyCode.X;
    public KeyCode RotationKey = KeyCode.Y;
    public bool isSelected = false;
    Outline outline;

    public float horizontalInput;
    public float verticalInput;
    public float moveSpeed = 200f;
    public float rotateSpeed = 100f;

    public CharacterMovement Player;
    public FurnitureDecorator playerFurnitureDecorator;

    Rigidbody rb;

    TMP_Text currentSelectedText;
    RaycastHit hit;

    void Start()
    {
        outline = this.GetComponent<Outline>();
        outline.enabled = false;

        Player = GameObject.FindAnyObjectByType<CharacterMovement>();
        playerFurnitureDecorator = Player.GetComponent<FurnitureDecorator>();
        rb = this.GetComponent<Rigidbody>();
        outline.OutlineColor = Color.yellow;
        currentSelectedText = GameObject.Find("CurrentSelectedText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHover && ( Input.GetButtonDown("Fire3") || Input.GetKeyDown(actionKey) ) && !isSelected && !playerFurnitureDecorator.currentInHand)
        {
            outline.OutlineColor = Color.blue;
            isSelected = true;
            currentSelectedText.fontSize = 30f;
            playerFurnitureDecorator.currentInHand = this.gameObject;
            currentSelectedText.text = this.gameObject.name.ToString() + " is Selected  \n Press Y To Rotate, Use Joystick To Move";

          
          
            return;
        }

        if(isSelected)
        {
            if(Input.GetButtonDown("Fire2") || Input.GetKey(RotationKey))
            {
                //Rotating If Y button is pressed
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }

            Player.lockMovement = true;
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Vector3 vel = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
            rb.velocity = vel;

            //Player.transform.DOLookAt(this.transform.position,1.0f,AxisConstraint.Z);
            
            if(isHover && (Input.GetButtonDown("Fire3") || Input.GetKeyDown(actionKey)))
            {
                Player.lockMovement = false;
                playerFurnitureDecorator.currentInHand = null;
                isSelected = false;
                outline.OutlineColor = Color.yellow;
                currentSelectedText.text = "";
            }
        }

    }

    public void ChangeHoverState(bool value)
    {
        isHover = value;

        if (isSelected) return;
        if (value)
            outline.enabled = true;
        else
            outline.enabled = false;
            
    }
}

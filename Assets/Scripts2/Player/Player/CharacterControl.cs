using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float speed = 6.0f;
    
    //public float sensitivity = 100f;
    float xRotation = 0f;

    private Transform cam;

    private CharacterController charController;

    void Start()
    {
    
        Cursor.lockState = CursorLockMode.Locked;

        charController = GetComponent<CharacterController>();

        cam = transform.Find("Camera");

    }
    
    void Update()
    {

        CameraMovement();


        Vector3 move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        charController.SimpleMove((Vector3.ClampMagnitude(move, 1.0f) * (Input.GetKey(KeyCode.LeftShift) ? speed * 1.4f : speed)));


    }

    void CameraMovement()
    {

        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //md = Vector2.Scale( md, new Vector2(sensitivity * Time.deltaTime, sensitivity * Time.deltaTime));

        xRotation -= md.y;

        cam.localRotation = Quaternion.Euler(Mathf.Clamp(xRotation, -70, 70), 0, 0);

        transform.transform.Rotate(Vector3.up * md.x);

    }

}
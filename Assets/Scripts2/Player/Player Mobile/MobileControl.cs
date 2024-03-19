using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileControl : MonoBehaviour, IDragHandler , IPointerUpHandler, IPointerDownHandler
{

    bool toqueAnalogic;
    bool toqueLook;

    private GameObject analogLeft;
    private GameObject lookDirection;
    public GameObject player;
    private GameObject camLook;

    private Image imgAnalog;
    private Image imgLook;

    private Image joystickImgAnalog;
    private Image joystickImgLook;

    private Vector3 inputVectorAnalogic;
    private Vector3 inputVectorLook;

    public float speed = 10.0f;
    private float straffe;
    private float translation;

    RectTransform rectTransform;
    Vector2 xyBase;

    private CharacterController charController;

    private void Awake()
    {

        Application.targetFrameRate = 60;


        analogLeft = transform.Find("AnalogLeft").gameObject;
        lookDirection = transform.Find("LookDirection").gameObject;
        camLook = player.transform.Find("Camera").gameObject;

        rectTransform = transform.GetComponent<RectTransform>();

        imgAnalog = analogLeft.GetComponent<Image>();
        joystickImgAnalog = analogLeft.transform.GetChild(0).GetComponent<Image>();

        imgLook = lookDirection.GetComponent<Image>();
        joystickImgLook = lookDirection.transform.GetChild(0).GetComponent<Image>();


        charController = player.GetComponent<CharacterController>();

        xyBase = new Vector2(camLook.transform.localRotation.eulerAngles.x, player.transform.localRotation.eulerAngles.y);

    }


    void Update()
    {
        
        //Look
        var md = new Vector2(inputVectorLook.z, inputVectorLook.x);

        camLook.transform.localRotation = Quaternion.Euler(xyBase.x + md.x * -60, 0, 0);

        player.transform.rotation =  Quaternion.Euler(0, xyBase.y + md.y * 180, 0);

        
        var md2 = new Vector2(inputVectorAnalogic.z, inputVectorAnalogic.x);

        Vector3 forwardMovement = player.transform.forward * md2.x;
        Vector3 rightMovement = player.transform.right * md2.y;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * (Input.GetKey(KeyCode.LeftShift) ? speed * 2: speed));

    }

    public virtual void OnDrag(PointerEventData ped)
    {

        bool _right = ped.position.x > (Screen.width / 2);
                
        Vector2 pos;

       if (!_right) 
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgAnalog.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x =  ((pos.x - (imgAnalog.rectTransform.rect.width / 2)) / imgAnalog.rectTransform.sizeDelta.x);
                pos.y =  ((pos.y + (imgAnalog.rectTransform.rect.height / 2)) / imgAnalog.rectTransform.sizeDelta.y);

                inputVectorAnalogic = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
                inputVectorAnalogic = (inputVectorAnalogic.magnitude > 1.0f) ? inputVectorAnalogic.normalized : inputVectorAnalogic;

                joystickImgAnalog.rectTransform.anchoredPosition = new Vector3(inputVectorAnalogic.x * (imgAnalog.rectTransform.sizeDelta.x / 3), inputVectorAnalogic.z * (imgAnalog.rectTransform.sizeDelta.y / 3));
            }
        }
        else if (_right) 
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgLook.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x =  ((pos.x - (imgLook.rectTransform.rect.width / 2)) / imgLook.rectTransform.sizeDelta.x);
                pos.y =  ((pos.y + (imgLook.rectTransform.rect.height / 2)) / imgLook.rectTransform.sizeDelta.y);

                inputVectorLook = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
                inputVectorLook = (inputVectorLook.magnitude > 1.0f) ? inputVectorLook.normalized : inputVectorLook;

                joystickImgLook.rectTransform.anchoredPosition = new Vector3(inputVectorLook.x * (imgLook.rectTransform.sizeDelta.x / 3), inputVectorLook.z * (imgLook.rectTransform.sizeDelta.y / 3));
            }        
        }



    }



    public virtual void OnPointerDown(PointerEventData ped)
    {


        bool direita = ped.position.x > (Screen.width / 2);

        if ((direita && toqueLook) || (!direita && toqueAnalogic))
            return;

            Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<Image>().rectTransform, ped.position, ped.pressEventCamera, out pos))
        {

            pos.x += (rectTransform.sizeDelta.x / 2);
            pos.y += (rectTransform.sizeDelta.y / 2);

            pos.x *= rectTransform.localScale.x;
            pos.y *= rectTransform.localScale.y;


            direita = pos.x > (Screen.width / 2);
            
            if (direita && !toqueLook)
            {
                toqueLook = true;
                xyBase = new Vector2(camLook.transform.localRotation.eulerAngles.x, player.transform.localRotation.eulerAngles.y);
                imgLook.transform.position = pos;
                OnDrag(ped);
            }
            else if(!direita && !toqueAnalogic)
            {
                toqueAnalogic = true;
                imgAnalog.transform.position = pos;
                OnDrag(ped);
            } 

        }


    }

    public virtual void OnPointerUp(PointerEventData ped)
    {


        bool direita = ped.position.x > (Screen.width / 2);

        if (!direita)
        {
            inputVectorAnalogic = Vector3.zero;
            toqueAnalogic = false;
            joystickImgAnalog.rectTransform.anchoredPosition = Vector3.zero;
        } else
        {
            toqueLook = false;
        }

        

    }

 

}

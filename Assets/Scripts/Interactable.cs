using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isHover = false;

   public void SetHoverTo(bool value)
    {
        isHover = value;
    }
    
}

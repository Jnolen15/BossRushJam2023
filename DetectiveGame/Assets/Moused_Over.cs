using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Moused_Over : MonoBehaviour, IPointerEnterHandler
{

    public GameObject MenuFolder;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("mousedOver");
    }

}

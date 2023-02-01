using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Moused_Over : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Highlight;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // Debug.Log("mousedOver");
        this.transform.parent.transform.parent.SetAsLastSibling();
        // ^this is currently just changing the order of the children of the main menu

        Highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Highlight.SetActive(false);
    }
}

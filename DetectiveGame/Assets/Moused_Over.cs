using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Moused_Over : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Highlight;
    public GameObject FolderCover;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<AudioSource>().Play();

        // Debug.Log("mousedOver");
        this.transform.parent.transform.parent.SetAsLastSibling();
        FolderCover.SetActive(false);

        Highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Highlight.SetActive(false);
    }
}

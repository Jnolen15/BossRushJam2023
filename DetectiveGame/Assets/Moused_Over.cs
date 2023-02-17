using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Moused_Over : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Highlight;
    public GameObject FolderCover;
    public bool FolderOn = true;
    public bool FolderOpenCall = true;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<AudioSource>().Play(); 

        // Debug.Log("mousedOver");
        this.transform.parent.transform.parent.SetAsLastSibling();
        FolderCover.SetActive(false);
        FolderOn = false;
        FolderOpenCall = true;

        Highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Highlight.SetActive(false);
    }

    public void CloseFolder()
    {
        FolderCover.SetActive(true);
        FolderOn = true;
    }

    public void Update()
    {
        if (this.transform.parent.transform.parent.GetSiblingIndex() < 3)
        {
            FolderCover.SetActive(true);
            FolderOn = true;
        }
    }
}

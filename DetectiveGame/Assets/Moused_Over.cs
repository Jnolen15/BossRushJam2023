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
    private int NumFolders = 5;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<AudioSource>().Play(); 

        // Debug.Log("mousedOver");
        this.transform.parent.transform.parent.SetAsLastSibling();
        FolderCover.SetActive(false);
        FolderOn = false;

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
        if (this.transform.parent.transform.parent.GetSiblingIndex() < NumFolders)
        {
            FolderCover.SetActive(true);
            FolderOn = true;
        }
    }
}

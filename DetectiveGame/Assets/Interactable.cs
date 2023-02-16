using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler
{
    private TableManager tm;
    public bool isDragging;
    [SerializeField] private PlayerController pc;

    private void Start()
    {
        tm = GetComponentInParent<TableManager>();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isDragging)
        {
            if (Input.mouseScrollDelta.y > 0.1f)
            {
                transform.Rotate(new Vector3(0, 0, 1) * 10f, Space.Self);
            }
            if (Input.mouseScrollDelta.y < -0.1f)
            {
                transform.Rotate(new Vector3(0, 0, -1) * 10f, Space.Self);
            }
        }

        if (transform.localPosition.x > 650 || transform.localPosition.x < -650)
        {
            Debug.Log("Reset cuz out of bounds");
            transform.localPosition = new Vector3(0, 0, 0);
        }

        // Fix for it randomly going to origin point of the world
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
    }

    // ========== MOUSE CONTROLS ==========
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit, Let go");
        isDragging = false;
        pc.DropEvidence();
    }

    // ========== DRAG DROP ==========
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Start dragging");
        tm.BringToTop(this.transform);
        isDragging = true;

        pc.PickupEvidence(this.gameObject);

        if (this.GetComponent<AudioSource>() != null)
            this.GetComponent<AudioSource>().Play();
        else
            Debug.LogError(name + " Does not have Audio Source!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDragging)
            this.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Let go");
        isDragging = false;
        pc.DropEvidence();
    }
}

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
    [SerializeField] private bool isDragging;

    private void Start()
    {
        tm = GetComponentInParent<TableManager>();
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
    }

    // ========== MOUSE CONTROLS ==========
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exit");
    }

    // ========== DRAG DROP ==========
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Start dragging");
        tm.BringToTop(this.transform);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Let go");
        isDragging = false;
    }
}

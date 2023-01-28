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

        pc.PickupEvidence(this.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDragging)
            this.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Let go");
        isDragging = false;
        pc.DropEvidence();
    }
}

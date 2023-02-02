using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject magGlass;
    [SerializeField] private GameObject magCam;

    [SerializeField] private GameObject table;
    [SerializeField] Transform tPosTable;
    [SerializeField] Transform tPosFull;
    [SerializeField] Transform tPosInterview;

    [SerializeField] private GameObject heldEvidence;

    public enum State
    {
        interview,
        full,
        table
    } 
    public State state;

    void Start()
    {
        magGlass.SetActive(false);
    }

    void Update()
    {
        // Magnifying glass
        if (Input.GetKey(KeyCode.LeftAlt) && state == State.table)
        {
            magGlass.SetActive(true);
            magGlass.transform.position = Input.mousePosition;
            magCam.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else
        {
            magGlass.SetActive(false);
        }

        SwitchPerspective();
    }

    private void SwitchPerspective()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (state != State.interview)
            {
                state--;
                Move();
            }
        }
                
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (state != State.table)
            {
                state++;
                Move();
            }
        }
    }

    private void Move()
    {
        if (state == State.interview)
        {
            table.transform.position = tPosInterview.position;
            table.transform.rotation = tPosInterview.rotation;
            Camera.main.orthographic = false;
        }
        else if (state == State.full)
        {
            table.transform.position = tPosFull.position;
            table.transform.rotation = tPosFull.rotation;
            Camera.main.orthographic = false;
        }
        else if (state == State.table)
        {
            table.transform.position = tPosTable.position;
            table.transform.rotation = tPosTable.rotation;
            Camera.main.orthographic = true;
        }
    }

    public void PickupEvidence(GameObject evidence)
    {
        heldEvidence = evidence;
        //Debug.Log("Picked up " + evidence.name);
    }
    public void DropEvidence()
    {
        if (heldEvidence == null)
            return;

        heldEvidence.GetComponent<Interactable>().isDragging = false;

        //Debug.Log("dropped " + heldEvidence.name);
        heldEvidence = null;
    }

    public GameObject GetEvidence()
    {
        return heldEvidence;
    }
}

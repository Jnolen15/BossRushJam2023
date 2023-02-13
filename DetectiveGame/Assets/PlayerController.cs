using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject magGlass;
    [SerializeField] private GameObject magCam;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject table;
    [SerializeField] Transform tPosTable;
    [SerializeField] Transform tPosFull;
    [SerializeField] Transform tPosInterview;

    [SerializeField] private GameObject heldEvidence;
    private GameManager gm;

    public enum State
    {
        interview,
        full,
        table
    } 
    public State state;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

        // Move camera
        SwitchPerspective();

        // Submit evidence
        if (heldEvidence != null)
        {
            //Debug.Log(heldEvidence.transform.localPosition.y);
            if (heldEvidence.transform.localPosition.y > 400f)
            {
                Debug.Log("EVIDENCE SUBMITED");
                gm.SubmitDocument(heldEvidence);
                heldEvidence.transform.localPosition = new Vector3(0, 0, 0);
                DropEvidence();
            }
        }
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
        DropEvidence();

        if (state == State.interview)
        {
            Camera.main.transform.position = tPosInterview.position;
            Camera.main.transform.rotation = tPosInterview.rotation;
            Camera.main.orthographic = false;
            HideDialogue(true);
        }
        else if (state == State.full)
        {
            Camera.main.transform.position = tPosFull.position;
            Camera.main.transform.rotation = tPosFull.rotation;
            Camera.main.orthographic = false;
            HideDialogue(true);
        }
        else if (state == State.table)
        {
            Camera.main.transform.position = tPosTable.position;
            Camera.main.transform.rotation = tPosTable.rotation;
            Camera.main.orthographic = true;
            HideDialogue(false);
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

    public void HideDialogue(bool doHide)
    {
        dialogue.SetActive(doHide);
    }
}

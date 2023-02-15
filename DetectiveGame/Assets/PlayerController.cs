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
    [SerializeField] private float lookSpeed;
    [SerializeField] private bool moving;

    public enum State
    {
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            state = State.table;
            Move();
        }
                
        else if (Input.GetKeyDown(KeyCode.W))
        {
            state = State.full;
            Move();
        }
    }

    private void Move()
    {
        if (moving)
            return;

        DropEvidence();

        if (state == State.table)
        {
            Debug.Log("Looking at Table");
            StartCoroutine(TransitionLook(tPosTable, true));
        }
        else if (state == State.full)
        {
            Debug.Log("Looking at Full");
            StartCoroutine(TransitionLook(tPosFull, false));
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

    IEnumerator TransitionLook(Transform lookto, bool atTable)
    {
        if (!atTable)
            Camera.main.orthographic = atTable;
        else
            HideDialogue(!atTable);

        moving = true;
        float time = 0;

        Vector3 startPos = Camera.main.transform.position;
        Quaternion startRot = Camera.main.transform.rotation;

        while (time < lookSpeed)
        {
            float t = time / lookSpeed;
            t = t * t * (3f - 2f * t);

            Camera.main.transform.position = Vector3.Lerp(startPos, lookto.position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startRot, lookto.rotation, t);

            time += Time.deltaTime;
            yield return null;
        }

        moving = false;

        if (atTable)
            Camera.main.orthographic = atTable;
        else
            HideDialogue(!atTable);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PresentZone : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Transform evidenceSpot;
    private PlayerController pc;
    public GameObject presentedEvidence;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pc.GetEvidence() == null)
            return;

        presentedEvidence = pc.GetEvidence();
        pc.DropEvidence();
        presentedEvidence.transform.position = evidenceSpot.position;
        presentedEvidence.transform.rotation = evidenceSpot.rotation;
        Present();
    }

    private void Present()
    {
        Debug.Log("Presenting Evidence: " + presentedEvidence.name);
    }
}

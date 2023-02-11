using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PresentZone : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject evidenceSpot;
    private PlayerController pc;
    private GameManager gm;
    public GameObject presentedEvidence;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        evidenceSpot.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTERES");

        if (pc.GetEvidence() == null)
            return;

        presentedEvidence = pc.GetEvidence();
        pc.DropEvidence();
        evidenceSpot.SetActive(true);
        evidenceSpot.GetComponent<Image>().sprite = presentedEvidence.GetComponent<Image>().sprite;
        presentedEvidence.transform.localPosition = new Vector3(0, 0, 0);
        Present();
    }

    private void Present()
    {
        Debug.Log("Presenting Evidence: " + presentedEvidence.name);
        gm.SubmitDocument(presentedEvidence);
    }

    public void Eject()
    {
        evidenceSpot.SetActive(false);
        presentedEvidence = null;
    }
}

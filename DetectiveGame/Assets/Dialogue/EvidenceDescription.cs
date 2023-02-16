using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidenceDescription : MonoBehaviour
{
    // Start is called before the first frame update
    public string evidence;
    public string description;
    public GameObject evidenceWindow;
    public GameObject evidenceName;
    public GameObject evidenceDescription;
    public PlayerController pc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        if (pc.state == PlayerController.State.table)
        {
            evidenceName.GetComponent<TextMeshProUGUI>().text = evidence;
            evidenceDescription.GetComponent<TextMeshProUGUI>().text = description;
            evidenceWindow.SetActive(true);
        }
    }

    public void OnMouseOff()
    {
        evidenceWindow.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DocName; // This is just for testing
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lostScreen;
    public float roundTime;
    public float looseScore;
    public float winScore;
    private ScoreManager score;
    private PresentZone presentZone;
    public Transform[] Documents;
    private Transform correctDoc;

    void Start()
    {
        score = this.GetComponent<ScoreManager>();
        presentZone = GameObject.FindGameObjectWithTag("PresentZone").GetComponent<PresentZone>();

        RequestDocument();
    }

    private void Update()
    {
        // Timer
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            //CurScore -= Time.deltaTime;
        }
        else if (score.CurScore > winScore)
            Win();
        else
            Lose();
    }

    private void RequestDocument()
    {
        var rand = Random.Range(0, Documents.Length);
        var doc = Documents[rand];

        DocName.text = doc.name;
        correctDoc = doc;
    }

    public void SubmitDocument(Transform evidence)
    {
        if (evidence == correctDoc)
        {
            Debug.Log("CORRECT! Documents " + evidence.name + " and " + correctDoc + " match!");
            score.Succeeded();
        } else
        {
            Debug.Log("WROG! *Skull emoji* Documents " + evidence.name + " and " + correctDoc + " don't match!");
            score.Failed();
        }

        presentZone.Eject();
        RequestDocument();
    }

    public void Lose()
    {
        Debug.Log("LOST");
        lostScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        Debug.Log("Won!");
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}

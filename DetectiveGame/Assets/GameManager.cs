using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DocName; // This is just for testing
    [SerializeField] private TextMeshProUGUI Rounds; // This is just for testing
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject startButton;
    public float roundTime;
    public float looseScore;
    public float winScore;
    public int numInteractions;
    public int curInteraction;
    public DialogueCode suspectsDialogue;
    public GameObject wrong;
    private ScoreManager score;
    private bool gameEnded = false;
    private Transform correctDoc;
    private PlayerController pc;


    void Start()
    {
        score = this.GetComponent<ScoreManager>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Timer
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            //CurScore -= Time.deltaTime;
        }
        else if (score.CurScore >= winScore)
            Win();
        else
            Lose();

        // Interactions
        if (curInteraction > numInteractions)
        {
            if (score.CurScore >= winScore)
                Win();
            else
                Lose();
        }
    }

    public void RequestDocument(Transform doc)
    {
        DocName.text = doc.name;
        correctDoc = doc;
    }

    public void SubmitDocument(GameObject evidenceWeWantToUse)
    {
        Debug.Log(evidenceWeWantToUse.name);
        if (evidenceWeWantToUse == suspectsDialogue.holdEvidence)
        {
            suspectsDialogue.currentDialogueFormat = "Evidence";
            suspectsDialogue.DisplayNextEvidenceSentence(evidenceWeWantToUse);
            score.Succeeded();
        } else
        {
            suspectsDialogue.currentDialogueFormat = "Evidence";
            evidenceWeWantToUse = wrong;
            suspectsDialogue.DisplayNextEvidenceSentence(evidenceWeWantToUse);
            
            score.Failed();
        }

        /*curInteraction++;
        Rounds.text = (curInteraction.ToString() + '/' + numInteractions.ToString());
        presentZone.Eject();
        RequestRandomDocument();*/
    }

    public void Lose()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        lostScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BringinSuspect()
    {
        pc.dialogueStarted = true;
        startButton.SetActive(false);
        dialogue.SetActive(true);
        dialogueManager.SetActive(true);
    }
}

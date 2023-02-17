using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DocName; // This is just for testing
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject reminder;
    [SerializeField] private GameObject clock;
    public float roundTime;
    public DialogueCode suspectsDialogue;
    public GameObject wrong;
    public int winScore;
    public int maxStrikes;
    private ScoreManager score;
    private bool gameEnded = false;
    private Transform correctDoc;
    private PlayerController pc;
    public bool interviewStarted;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;

    void Start()
    {
        Time.timeScale = 1;

        score = this.GetComponent<ScoreManager>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Wait until prep stage is over
        if (!interviewStarted)
            return;

        // Timer
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            int forClock = Mathf.RoundToInt(roundTime);
            clock.GetComponent<TextMeshPro>().text = forClock.ToString();
            //CurScore -= Time.deltaTime;
        }
        // If time runs out, Lose
        else
            Lose();

        // If win score is met
        if (score.curSuccessNum >= winScore)
        {
            Win();
        }

        // If strikes are full
        if (score.curStrikeNum == maxStrikes)
        {
            Lose();
        }
    }

    public void BringinSuspect()
    {
        interviewStarted = true;
        pc.dialogueStarted = true;
        startButton.SetActive(false);
        reminder.SetActive(false);
        dialogue.SetActive(true);
        dialogueManager.SetActive(true);
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StartTimed();
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
    }

    public void Lose()
    {
        if (gameEnded)
            return;

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
        loseSound.Play();

        StartCoroutine(EndGame(lostScreen));
    }

    public void Win()
    {
        if (gameEnded)
            return;

        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
        winSound.Play();

        StartCoroutine(EndGame(winScreen));
    }

    IEnumerator EndGame(GameObject screen)
    {
        gameEnded = true;

        yield return new WaitForSecondsRealtime(4f);

        dialogue.SetActive(false);
        dialogueManager.SetActive(false);
        screen.SetActive(true);
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
}

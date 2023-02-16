using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueCode : MonoBehaviour
{
    // Class that lets writers do questioning dialogue base on which line the suspect is on
    // Example: Character Dialogue [0] is Questioning Dialogue [0]
    public GameManager gm;
    public GameObject daName;
    public GameObject daDialogue;
    public GameObject daQuestions;
    public string DaEvidence;
    public GameObject evidenceWeAreUsing;
    public GameObject[] evidence;


    [System.Serializable]
    public class QuestioningDialogueClass
    {
        
        public string name;
        public GameObject evidence;

        [TextArea(4, 10)]
        public string sentences;

        public bool endOfDialogue;
        public bool endOfPhase;
    }
    [System.Serializable]
    public class EvidenceDialogueClass
    {

        public string name;

        [TextArea(4, 10)]
        public string sentences;

        public bool endOfDialogue;
        public bool endOfPhase;
        public bool explodeQuestionMark;
    }

    // Use to make an array of array of questioningDialogue classes
    [System.Serializable]
    public class QuestionSetUp
    {
        public string Question;
        public QuestioningDialogueClass[] questioningLines;
    }
    [System.Serializable]
    public class EvidenceSetUp
    {
        public GameObject Evidence;
        public EvidenceDialogueClass[] evidenceLines;
    }

    [System.Serializable]
    public class Phases
    {
        public string currentPhase;
        public DialogueClass[] dialogueClasses;
        
        
        
    }
    //Will change to an array of objects

    public Phases[] allPhases;


    // Three set of Dialogue arrays, for suspects alibi, questioning the alibi, and showing the right and wrong evidence dialogue
    public QuestionSetUp[] questioningLines;
    public EvidenceSetUp[] evidenceDialogueLines;


    //this dictionary hold the info of the characterDialogue sentence as the key and the value is the questioning dialogue class
    //(Dictionary <Character dialogue (key), questioning dialogue (value)>
    public Dictionary<string, QuestioningDialogueClass[]> correctQuestioning = new Dictionary<string, QuestioningDialogueClass[]>();
    
    // The dictionary key holds the evidence so we can do the right dilaogue if the player shows the correct evidence or wrong
    // We dod this so we can output the correct value of evidence Dialogue
    public Dictionary<GameObject, EvidenceDialogueClass[]> correctEvidence = new Dictionary<GameObject, EvidenceDialogueClass[]>();

    //private bool endingThePhase = false;
    private int forNormalLines = 0;
    private int forQuestioningLines = 0;
    private int forEvidenceLines = 0;
    public GameObject holdEvidence;
    public string currentDialogueFormat;
    private string questionString;
    public int currentPhaseCounter = 0;
    public string checkCurrentPhase;

    public PlayerController pc;

    private IEnumerator currDlogCoroutine;
    private bool isCurrentLineFinished;


    void Start()
    {

        currentDialogueFormat = "Normal";
        
        for (int i = 0; i < questioningLines.Length; i++)
        {
            
            correctQuestioning.Add(questioningLines[i].Question, questioningLines[i].questioningLines);
        }
        
        for(int i = 0; i < evidenceDialogueLines.Length; i++)
        {
            correctEvidence.Add(evidenceDialogueLines[i].Evidence, evidenceDialogueLines[i].evidenceLines);
        }
        
        //Debug.Log(forQuestioningDialogue.Length);
        /*
        foreach (KeyValuePair<GameObject, EvidenceDialogueClass[]> attachStat in correctEvidence)
        {

            Debug.Log(attachStat.Key);
            //Debug.Log(attachStat.Value.Length);
            //foreach(QuestioningDialogueClass theQuestion in attachStat.Value)
            //{
                //Debug.Log(theQuestion.name);
            //    Debug.Log(theQuestion.sentences);
            //}
            //Debug.Log(attachStat.Value);
        }
        */
        DisplayNextSentence(currentPhaseCounter, forNormalLines);
        


    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void OnMouseDownQuestion(GameObject question)
    {
        currentDialogueFormat = "Questioning";
        questionString = question.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(questionString);
        DisplayNextQuestioningSentence(questionString, forQuestioningLines);
    }

    public void OnMouseDownContinue()
    {
        switch (currentDialogueFormat)
        {

            case "Normal":
                if (pc.state != PlayerController.State.table)
                {

                    //forNormalLines++;
                    DisplayNextSentence(currentPhaseCounter, forNormalLines);

                }
                break;
            case "Questioning":
                //DisplayNextQuestioningSentence(characterDialogue[forNormalLines].sentences, forQuestioningLines);
                if (pc.state != PlayerController.State.table)
                {
                    DisplayNextQuestioningSentence(questionString, forQuestioningLines);
                }
                break;
            case "Evidence":
                //DisplayNextEvidenceSentence(characterDialogue[forNormalLines].evidence, forNormalLines, forQuestioningLines);
                if (pc.state != PlayerController.State.table)
                {
                    DisplayNextEvidenceSentence(holdEvidence);
                }
                break;
        }
    }
    public void DisplayNextSentence(int currentPhase,int nextLine)
    {

        //If we are at the end of the dialouge than return to the first line
        Debug.Log(forNormalLines);
        checkCurrentPhase = "Normal";
        daName.GetComponent<TextMeshProUGUI>().text = allPhases[currentPhase].dialogueClasses[nextLine].name;
        //daDialogue.GetComponent<TextMeshProUGUI>().text = allPhases[currentPhaseCounter].dialogueClasses[nextLine].sentences;
        SendToTextBox(allPhases[currentPhaseCounter].dialogueClasses[nextLine].sentences);
        holdEvidence = allPhases[currentPhaseCounter].dialogueClasses[nextLine].evidence;
        int questionActivater = 0;
        if (allPhases[currentPhaseCounter].dialogueClasses[nextLine].questions.Length > 0)
        {
            
            foreach (string numQuestion in allPhases[currentPhaseCounter].dialogueClasses[nextLine].questions)
            {
                
                GameObject theQuestion = daQuestions.transform.GetChild(questionActivater).gameObject;
                theQuestion.SetActive(true);
                theQuestion.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = numQuestion;
                //theQuestion.GetComponent<TextMeshProUGUI>().text = numQuestion;
                //theQuestion.SetActive(true);
                questionActivater++;
            }
        }
        else
        {
            daQuestions.transform.GetChild(0).gameObject.SetActive(false);
            daQuestions.transform.GetChild(1).gameObject.SetActive(false);
            daQuestions.transform.GetChild(2).gameObject.SetActive(false);
        }
        bool end = allPhases[currentPhaseCounter].dialogueClasses[nextLine].endOfDialogue;
        bool endPhse = allPhases[currentPhaseCounter].dialogueClasses[nextLine].endOfPhase;
        if (end)
        {
            forNormalLines = 0;
        }
        else
        {
            forNormalLines++;
        }
        if (endPhse)
        {
            currentDialogueFormat = "Normal";
            currentPhaseCounter++;
            forNormalLines = 0;
            forQuestioningLines = 0;
            forEvidenceLines = 0;

        }
        //Deals with the images of the dialogue

        //enders = characterDialogue[nextLine].endOfDialogue;



    }
    public void DisplayNextQuestioningSentence(string theQuestion, int nextLine)
    {

        //If we are at the end of the dialouge than return to the first line
        checkCurrentPhase = "Questioning";
        Debug.Log(theQuestion);
        QuestioningDialogueClass[] theQuestioning = correctQuestioning[theQuestion];
        daQuestions.transform.GetChild(0).gameObject.SetActive(false);
        daQuestions.transform.GetChild(1).gameObject.SetActive(false);
        daQuestions.transform.GetChild(2).gameObject.SetActive(false);

        daName.GetComponent<TextMeshProUGUI>().text = theQuestioning[nextLine].name;
            //Debug.Log(theQuestioning[nextLine].name);
        //daDialogue.GetComponent<TextMeshProUGUI>().text = theQuestioning[nextLine].sentences;
        SendToTextBox(theQuestioning[nextLine].sentences);
        holdEvidence = theQuestioning[nextLine].evidence;
        bool enders = theQuestioning[nextLine].endOfDialogue;
        bool endingThePhase = theQuestioning[nextLine].endOfPhase;

        if (enders)
        {
            currentDialogueFormat = "Normal";
            forQuestioningLines = 0;
            forNormalLines = 0;
        }
        else
        {
            forQuestioningLines++;
        }
        if (endingThePhase)
        {
            currentDialogueFormat = "Normal";
            currentPhaseCounter++;
            forNormalLines = 0;
            forQuestioningLines = 0;
            if(theQuestioning[nextLine].sentences == "")
            {
                DisplayNextSentence(currentPhaseCounter, forNormalLines);
            }
            
        }
        
    }
    public void DisplayNextEvidenceSentence(GameObject evidence)
    {


        
        daQuestions.transform.GetChild(0).gameObject.SetActive(false);
        daQuestions.transform.GetChild(1).gameObject.SetActive(false);
        daQuestions.transform.GetChild(2).gameObject.SetActive(false);
        EvidenceDialogueClass[] theEvidence = correctEvidence[evidence];

        daName.GetComponent<TextMeshProUGUI>().text = theEvidence[forEvidenceLines].name;
        //daDialogue.GetComponent<TextMeshProUGUI>().text = theEvidence[forEvidenceLines].sentences;
        SendToTextBox(theEvidence[forEvidenceLines].sentences);

        bool enders = theEvidence[forEvidenceLines].endOfDialogue;
        bool endPhse = theEvidence[forEvidenceLines].endOfPhase;
        if (enders)
        {
            forEvidenceLines = 0;
            forNormalLines = 0;
            forQuestioningLines = 0;
            currentDialogueFormat = checkCurrentPhase;
        }
        else
        {
            forEvidenceLines++;
        }
        if (endPhse)
        {
            currentDialogueFormat = "Normal";
            currentPhaseCounter++;
            forNormalLines = 0;
            forQuestioningLines = 0;
            forEvidenceLines = 0;

        }



    }


    private void SendToTextBox(string text)
    {
        // stop any coroutines currently running so we can run this one
        if (currDlogCoroutine != null)
        {
            StopCoroutine(currDlogCoroutine);
        }

        // run our coroutine
        currDlogCoroutine = TypeLineCharacters(text);
        StartCoroutine(currDlogCoroutine);
    }

    IEnumerator TypeLineCharacters(string line)
    {
        var dlogTextBox = daDialogue.GetComponent<TextMeshProUGUI>();
        var criminalanimator = GameObject.FindGameObjectWithTag("Criminal").GetComponent<Animator>();
        criminalanimator.SetBool("talking", true);
        isCurrentLineFinished = false;

        // empty the text box
        dlogTextBox.text = "";
        int charCount = 0;

        // divide the line up into individual letters
        char[] letterArray = line.ToCharArray();

        // pause and then add in letters with pauses between each addition
        for (int i = 0; i < line.Length; i++)
        {
            if (!isCurrentLineFinished)
            {
                // add to the textbox letter by letter
                dlogTextBox.text += letterArray[i];

                // play typing sfx
                if (0 == charCount % 2 && letterArray[i] != ' ')
                {
                    // PLAY SOUND HERE
                    //uiReferences.boop.Play();
                }
                charCount++;

                // wait before typing next character
                yield return new WaitForSecondsRealtime(0.01f);
            }
            else
            {
                // line is supposed to be finished, so completely type line and put loop at the end
                dlogTextBox.text = line;
                //uiReferences.boop.Play();
                i = line.Length;
            }
        }

        criminalanimator.SetBool("talking", false);
        isCurrentLineFinished = true;
    }
}

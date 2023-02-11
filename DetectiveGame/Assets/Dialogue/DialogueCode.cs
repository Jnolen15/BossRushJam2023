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
        
        
        switch (currentDialogueFormat){

            case "Normal":
                if (Input.GetKeyDown(KeyCode.A))
                {
                    
                    //forNormalLines++;
                    DisplayNextSentence(currentPhaseCounter, forNormalLines);
                    
                }
                break;
            case "Questioning":
                //DisplayNextQuestioningSentence(characterDialogue[forNormalLines].sentences, forQuestioningLines);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    DisplayNextQuestioningSentence(questionString, forQuestioningLines);
                }
                break;
            case "Evidence":
                //DisplayNextEvidenceSentence(characterDialogue[forNormalLines].evidence, forNormalLines, forQuestioningLines);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    DisplayNextEvidenceSentence(holdEvidence);
                }
            break;
        }
        /*
        if(Input.GetKeyDown(KeyCode.E) && holdEvidence == evidenceWeAreUsing)
        {
            currentDialogueFormat = "Evidence";
            //DisplayNextEvidenceSentence(holdEvidence, checkCurrentPhase, forEvidenceLines);
        }
        if (Input.GetKeyDown(KeyCode.E) && (holdEvidence != evidenceWeAreUsing || holdEvidence == null))
        {
            currentDialogueFormat = "Evidence";
            //holdEvidence = "Incorrect";
            //DisplayNextEvidenceSentence(holdEvidence, checkCurrentPhase, forEvidenceLines);
        }
        */
        Debug.Log(holdEvidence);

    }
    public void OnMouseDown(GameObject question)
    {
        currentDialogueFormat = "Questioning";
        questionString = question.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(questionString);
        DisplayNextQuestioningSentence(questionString, forQuestioningLines);
    }
    public void DisplayNextSentence(int currentPhase,int nextLine)
    {

        //If we are at the end of the dialouge than return to the first line
        Debug.Log(forNormalLines);
        checkCurrentPhase = "Normal";
        daName.GetComponent<TextMeshProUGUI>().text = allPhases[currentPhase].dialogueClasses[nextLine].name;
        daDialogue.GetComponent<TextMeshProUGUI>().text = allPhases[currentPhaseCounter].dialogueClasses[nextLine].sentences;
        //holdEvidence = null;
        int questionActivater = 0;
        if (allPhases[currentPhaseCounter].dialogueClasses[nextLine].questions.Length > 0)
        {
            
            foreach (string numQuestion in allPhases[currentPhaseCounter].dialogueClasses[nextLine].questions)
            {
                
                GameObject theQuestion = daQuestions.transform.GetChild(questionActivater).gameObject;
                theQuestion.GetComponent<TextMeshProUGUI>().text = numQuestion;
                theQuestion.SetActive(true);
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
        if (end)
        {
            forNormalLines = 0;
        }
        else
        {
            forNormalLines++;
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
        daDialogue.GetComponent<TextMeshProUGUI>().text = theQuestioning[nextLine].sentences;
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
            
        }
        
    }
    public void DisplayNextEvidenceSentence(GameObject evidence)
    {


        
        daQuestions.transform.GetChild(0).gameObject.SetActive(false);
        daQuestions.transform.GetChild(1).gameObject.SetActive(false);
        daQuestions.transform.GetChild(2).gameObject.SetActive(false);
        EvidenceDialogueClass[] theEvidence = correctEvidence[evidence];

        daName.GetComponent<TextMeshProUGUI>().text = theEvidence[forEvidenceLines].name;
        daDialogue.GetComponent<TextMeshProUGUI>().text = theEvidence[forEvidenceLines].sentences;
        

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


}
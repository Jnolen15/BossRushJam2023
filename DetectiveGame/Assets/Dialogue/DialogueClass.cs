using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueClass
{
    //this will later become a variable that gets the object of the evidence
    public GameObject evidence;
    
    public string name;

    [TextArea(4, 10)]
    public string sentences;

    public string[] questions;

    public bool endOfDialogue;
    public bool endOfPhase;
}
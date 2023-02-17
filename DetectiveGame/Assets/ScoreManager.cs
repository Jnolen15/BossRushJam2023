using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameObject strike;
    [SerializeField] private GameObject strikeZone;
    [SerializeField] private GameObject curStrike;
    public int curStrikeNum;
    public int curSuccessNum;
    [SerializeField] private bool hasLost;
    [SerializeField] private bool hasWon;

    void Start()
    {
        gm = this.GetComponent<GameManager>();

        for(int i = 0; i < gm.maxStrikes; i++)
        {
            Instantiate(strike, strikeZone.transform);
        }

        curStrike = strikeZone.transform.GetChild(curStrikeNum).gameObject;
    }

    public void Succeeded()
    {
        curStrike.transform.GetChild(0).gameObject.SetActive(true);
        curStrikeNum++;
        curSuccessNum++;
        if (curStrikeNum < gm.maxStrikes)
            curStrike = strikeZone.transform.GetChild(curStrikeNum).gameObject;
        // If its the last strike and the player has no won, give them an extra chance
        else if (curSuccessNum < gm.winScore)
        {
            Debug.Log("Another Chance!");
            Instantiate(strike, strikeZone.transform);
            gm.maxStrikes++;
            curStrike = strikeZone.transform.GetChild(curStrikeNum).gameObject;
        }
    }

    public void Failed()
    {
        curStrike.transform.GetChild(1).gameObject.SetActive(true);
        curStrikeNum++;
        if(curStrikeNum < gm.maxStrikes)
            curStrike = strikeZone.transform.GetChild(curStrikeNum).gameObject;
    }
}

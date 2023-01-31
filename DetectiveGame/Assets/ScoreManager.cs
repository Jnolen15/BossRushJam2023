using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Image scorebar;
    [SerializeField] private Image loosebar;
    [SerializeField] private Image winbar;
    [SerializeField] private float roundTime;
    [SerializeField] private int maxScore;
    [SerializeField] private float looseScore;
    [SerializeField] private float winScore;
    [SerializeField] private int startScore;
    [SerializeField] private float curScore;
    public float CurScore
    {
        get { return curScore; }
        set
        {
            curScore = value;
            scorebar.fillAmount = curScore / maxScore;
            Debug.Log((curScore / maxScore));
        }
    }

    void Start()
    {
        CurScore = startScore;

        loosebar.fillAmount = looseScore / maxScore;
        winbar.fillAmount = winScore / maxScore;
    }

    void Update()
    {
        // Timer
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            //CurScore -= Time.deltaTime;
        }
        else
            Debug.Log("LOST");

        // For testing
        if (Input.GetKeyDown(KeyCode.J))
            Succeeded();
        
        if (Input.GetKeyDown(KeyCode.K))
            Failed();

        // Loosing
        if (curScore < looseScore)
        {
            Debug.Log("LOST");
        }
    }

    public void Succeeded()
    {
        CurScore += 25;

        if (CurScore > maxScore)
            CurScore = maxScore;
    }

    public void Failed()
    {
        CurScore -= 25;

        if (CurScore < 0)
            CurScore = 0;
    }
}

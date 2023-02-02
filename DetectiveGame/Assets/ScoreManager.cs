using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private Image scorebar;
    [SerializeField] private Image loosebar;
    [SerializeField] private Image winbar;
    [SerializeField] private int maxScore;
    [SerializeField] private int startScore;
    [SerializeField] private float curScore;
    [SerializeField] private bool hasLost;
    [SerializeField] private bool hasWon;
    public float CurScore
    {
        get { return curScore; }
        set
        {
            curScore = value;
            scorebar.fillAmount = curScore / maxScore;
        }
    }

    void Start()
    {
        gm = this.GetComponent<GameManager>();

        CurScore = startScore;

        loosebar.fillAmount = gm.looseScore / maxScore;
        winbar.fillAmount = gm.winScore / maxScore;
    }

    void Update()
    {
        if(!hasLost || !hasWon)
        {
            // For testing
            if (Input.GetKeyDown(KeyCode.J))
                Succeeded();

            if (Input.GetKeyDown(KeyCode.K))
                Failed();

            // Losing
            if (curScore < gm.looseScore)
            {
                gm.Lose();
            }
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

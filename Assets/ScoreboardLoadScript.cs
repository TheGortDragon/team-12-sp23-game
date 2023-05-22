using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardLoadScript : MonoBehaviour
{
    ScoreManagerScript scoreManager = null;
    string[,,] scores;
    bool scoresRetrieved = false;

    public int curLevel = 1;
    public const int LEVEL_COUNT = 3;
    public const int SCORE_COUNT = 10;

    public TMP_Text namesText = null;
    public TMP_Text scoresText = null;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            namesText = GameObject.Find("Names").GetComponent<TextMeshProUGUI>();;
            scoresText = GameObject.Find("Scores").GetComponent<TextMeshProUGUI>();;
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
            scoreManager.getScores();
        } catch(NullReferenceException e)
        {
            Debug.Log("This should never happen.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreManager.responseValid() && !scoresRetrieved)
        {
            scoresRetrieved = true;
            scores = scoreManager.getResponse();
            Debug.Log("Scores loaded.");
            namesText.text = constructNames(curLevel);
            scoresText.text = constructScores(curLevel);
        }
        if(!scoresRetrieved)
        {
            Debug.Log("Scores not yet loaded.");
        }
    }

    void loadScores(int level)
    {
        namesText.text = constructNames(level);
        scoresText.text = constructScores(level);
    }

    string constructNames(int level)
    {
        if(!scoresRetrieved)
        {
            return ""; //bad
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(scores[level - 1,0,0]);
        for(int i = 1; i < SCORE_COUNT; i++)
        {
            sb.Append("\n");
            sb.Append(scores[level - 1,i,0]);
        }

        return sb.ToString();
    }

    string constructScores(int level)
    {
        if(!scoresRetrieved)
        {
            return ""; //also bad
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(scores[level - 1,0,1]);
        for(int i = 1; i < SCORE_COUNT; i++)
        {
            sb.Append("\n");
            sb.Append(scores[level - 1,i,1]);
        }

        return sb.ToString();
    }

    string[,] getScoresForLevel(int level)
    {
        string[,] scoresForLevel = new string[10, 2];
        for(int i = 0; i < 10; i++)
        {
            scoresForLevel[i,0] = scores[level,i,0];
            scoresForLevel[i,1] = scores[level,i,1];
        }
        return scoresForLevel;
    }

    public void nextPage()
    {
        curLevel++;
        if(curLevel > LEVEL_COUNT)
        {
            curLevel = 1;
        }
        loadScores(curLevel);

    }

    public void prevPage()
    {
        curLevel--;
        if(curLevel < 1)
        {
            curLevel = LEVEL_COUNT;
        }
        loadScores(curLevel);
    }
}

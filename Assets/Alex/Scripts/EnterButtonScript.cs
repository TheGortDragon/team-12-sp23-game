using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterButtonScript : MonoBehaviour
{

    ScoreManagerScript scoreManager = null;
    TimeManagerScript timeManager = null;
    public TMP_InputField input = null;
    string name = "";
    bool foundManagers = true;
    // Start is called before the first frame update
    void Start()
    {
        try{
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
            timeManager = GameObject.Find("TimeManager").GetComponent<TimeManagerScript>();
        } catch(NullReferenceException e)
        {
            foundManagers = false;
            Debug.Log("Could not find one of the managers.");
        }
        Button btn = GetComponent<Button>();
        input = GameObject.Find("NameEntry").GetComponent<TMP_InputField>();
		btn.onClick.AddListener(TaskOnClick);
        //Debug.Log(GameObject.Find("NameEntry"));
        input.onValueChanged.AddListener(OnInputFieldValueChanged);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        Debug.Log("Enter pressed.");
        if(foundManagers)
        {
            int level = timeManager.getLevel();
            int score = timeManager.getTimeScore();
            timeManager.kill();
            string scoreName = this.name;
            scoreManager.sendScore(scoreName, score, level);
            //go to next level scene
            SceneManager.LoadScene("ScoreboardScreen", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Manager not available.");
        }
    }

    public void OnInputFieldValueChanged(string value)
    {
        name = value;
    }
}

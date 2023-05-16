using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterButtonScript : MonoBehaviour
{

    ScoreManagerScript scoreManager = null;
    TimeManagerScript timeManager = null;
    public TMP_InputField input = null;
    string name = "";
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManagerScript>();
        Button btn = GetComponent<Button>();
        input = GameObject.Find("NameEntry").GetComponent<TMP_InputField>();
		btn.onClick.AddListener(TaskOnClick);
        Debug.Log(GameObject.Find("NameEntry"));
        input.onValueChanged.AddListener(OnInputFieldValueChanged);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        Debug.Log("Enter pressed.");
        int level = timeManager.getLevel();
        int sccore = timeManager.getTimeScore();
        string scoreName = this.name;
    }

    public void OnInputFieldValueChanged(string value)
    {
        name = value;
    }
}

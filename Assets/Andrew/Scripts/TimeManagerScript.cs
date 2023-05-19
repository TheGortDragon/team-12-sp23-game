using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class TimeManagerScript : MonoBehaviour
{
    public GameObject tmp = null;
    public TMP_Text tmp_text = null;
    public int startTime = 0;
    public int endTime = 0;
    public int mostRecentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GameObject.Find("TimeText");
        tmp_text = tmp.GetComponent<TextMeshProUGUI>();
        timeStart();
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("currTime is " + currTime);
        
        //Debug.Log("formattedTime is " + formattedTime);
        if (endTime == 0 && !Bounce.playerRespawned) {
            int currTime = (int)(Time.time * 1000) - startTime;
            string formattedTime = getFormattedTime(currTime);
            tmp_text.text = formattedTime;
        } else if (Bounce.playerRespawned) {
            timeStart();
        }
    }

    string getFormattedTime(int time)
    {
        StringBuilder sb = new StringBuilder();
        string s = (time / 1000).ToString();
        string ms = (time % 1000).ToString();
        
        int charsToAdd = 3 - s.Length;
        for(int i = 0; i < charsToAdd; i++)
        {
            sb.Append("0");
        }
        sb.Append(s);
        sb.Append(":");
        charsToAdd = 3 - ms.Length;
        for(int i = 0; i < charsToAdd; i++)
        {
            sb.Append("0");
        }
        sb.Append(ms);
        return sb.ToString();
    }

    //starts or resets the timer
    public void timeStart()
    {
        Debug.Log("Time set to 0");
        tmp_text.text = "000:000";
        startTime = (int) (Time.time * 1000);
        Debug.Log("startTime is " + startTime);
    }

    //logs the end time
    public void timeStop()
    {
        Debug.Log("Logged end time");
        endTime = (int) (Time.time * 1000) - startTime;
    }

    public int getTimeScore()
    {
        return endTime;
    }

    public void setLevel(int level)
    {
        mostRecentLevel = level;
    }

    public int getLevel()
    {
        return mostRecentLevel;
    }
}

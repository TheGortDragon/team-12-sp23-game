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
    // Start is called before the first frame update
    void Start()
    {
        tmp = GameObject.Find("TimeText");
        tmp_text = tmp.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int currTime = (int) (Time.time * 1000) - startTime;
        tmp_text.text = getFormattedTime(currTime);
        
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
        startTime = (int) (Time.time * 1000);
    }

    //logs the end time
    public void timeStop()
    {
        endTime = (int) (Time.time * 1000) - startTime;
    }
}

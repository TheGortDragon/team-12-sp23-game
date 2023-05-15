using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManagerScript : MonoBehaviour
{
    public string url = "associated-gr.at.ply.gg:53538/scores";
    public string testJSON = "{\"Name\":\"Alex\",\"Score\":234846}";
    public string mostRecentResponse = "";
    public const int LEVEL_COUNT = 3; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Sending POST request.");
            sendPOSTRequest(writeJSON("Bob", 234846, 1));  
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Sending GET request.");
            //sendGETRequest();
            getScores();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            string[,,] scores = getResponse();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 10; i++)
            {
                sb.Append(scores[0, i, 0]);
                sb.Append(" ");
                sb.Append(scores[0, i, 1]);
            }
            Debug.Log(sb.ToString());
        }
    }

    public void sendScore(string name, int score, int level)
    {
        string newjson = writeJSON(name, score, level);
        newjson = sanitizeString(newjson);
        sendPOSTRequest(newjson);
    }

    public void getScores()
    {
        sendGETRequest();        
    }

    public string[,,] getResponse()
    {
        Scoreboard s = JsonUtility.FromJson<Scoreboard>(mostRecentResponse);
        
        string[] names1 = s.Names1.Split(',');
        string[] values1 = s.Scores1.Split(',');
        string[] names2 = s.Names2.Split(',');
        string[] values2 = s.Scores2.Split(',');
        string[] names3 = s.Names3.Split(',');
        string[] values3 = s.Scores3.Split(',');
        string[,,] scoreboard = new string[3, 10, 2];


        for(int i = 0; i < 10; i++)
        {
            scoreboard[0, i, 0] = names1[i];
            scoreboard[0, i, 1] = values1[i];
        }
        for(int i = 0; i < 10; i++)
        {
            scoreboard[1, i, 0] = names2[i];
            scoreboard[1, i, 1] = values2[i];
        }
        for(int i = 0; i < 10; i++)
        {
            scoreboard[2, i, 0] = names3[i];
            scoreboard[2, i, 1] = values3[i];
        }
        return scoreboard;
    }


    string sanitizeString(string text)
    {
        return text.Replace("\r", "").Replace("\n", "");
    }

    string writeJSON(string name, int score, int level)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{\"Name\":\"");
        sb.Append(name);
        sb.Append("\",\"Score\":");
        sb.Append(score);
        sb.Append(",\"Level\":");
        sb.Append(level);
        sb.Append("}");
        Debug.Log(sb.ToString());
        return sb.ToString();

    }

    

    void sendGETRequest()
    {
        StartCoroutine(getRequest(url));
    }

    void sendPOSTRequest(string json)
    {
        StartCoroutine(postRequest(url, json));
    }

    IEnumerator getRequest(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Send the request
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request success.");
                // Request succeeded
                mostRecentResponse = webRequest.downloadHandler.text;
                // Process the response data here
            }
            else
            {
                // Request failed
                Debug.Log("Error: " + webRequest.error);
            }
        }
    }

    IEnumerator postRequest(string url, string json)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            // Set the request headers
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Set the request body (JSON data)
            byte[] bodyData = new System.Text.UTF8Encoding().GetBytes(json);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyData);

            // Send the request
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request success");
            }
            else
            {
                // Request failed
                Debug.Log("Error: " + webRequest.error);
            }
        }
    }
}

// Custom tuple class
public class Tuple<T1, T2>
{
    public T1 name { get; }
    public T2 value { get; }

    public Tuple(T1 item1, T2 item2)
    {
        name = item1;
        value = item2;
    }
}

public class Scoreboard
{
    public string Names1;
    public string Scores1;
    public string Names2;
    public string Scores2;
    public string Names3;
    public string Scores3;
}

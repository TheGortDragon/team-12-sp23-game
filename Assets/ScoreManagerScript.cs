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
            sendPOSTRequest(writeJSON("Bob", 234846));  
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Sending GET request.");
            //sendGETRequest();
            getScores();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            Tuple<string, string>[] scores = getResponse();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 10; i++)
            {
                sb.Append(scores[i].name);
                sb.Append(" ");
                sb.Append(scores[i].value);
                sb.Append("\n");
            }
            Debug.Log(sb.ToString());
        }
    }

    public void sendScore(string name, int score)
    {
        string newjson = writeJSON(name, score);
        newjson = sanitizeString(newjson);
        sendPOSTRequest(newjson);
    }

    public void getScores()
    {
        sendGETRequest();        
    }

    public Tuple<string, string>[] getResponse()
    {
        Tuple<string, string>[] scores = new Tuple<string, string>[10];
        Scoreboard s = JsonUtility.FromJson<Scoreboard>(mostRecentResponse);

        string[] names = s.Names.Split(',');
        string[] values = s.Scores.Split(',');
        for(int i = 0; i < 10; i++)
        {
            Tuple<string, string> score = new Tuple<string, string>(names[i], values[i]);
            scores[i] = score;
        }
        return scores;
    }


    string sanitizeString(string text)
    {
        return text.Replace("\r", "").Replace("\n", "");
    }

    string writeJSON(string name, int score)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{\"Name\":\"");
        sb.Append(name);
        sb.Append("\",\"Score\":");
        sb.Append(score);
        sb.Append("}");
        return sb.ToString();

    }

    

    void sendGETRequest()
    {
        StartCoroutine(getRequest(url));
        //Debug.Log(mostRecentResponse);
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
    public string Names;
    public string Scores;
}

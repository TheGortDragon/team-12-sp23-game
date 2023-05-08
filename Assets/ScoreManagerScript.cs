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
            sendGETRequest();
        }
    }

    string sanitizeString(string text)
    {
        return "";
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
        Debug.Log(mostRecentResponse);
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

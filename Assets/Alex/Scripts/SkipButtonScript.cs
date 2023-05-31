using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButtonScript : MonoBehaviour
{
    TimeManagerScript timeManager = null;
    bool foundManagers = true;

    // Start is called before the first frame update
    void Start()
    {
        try{
            timeManager = GameObject.Find("TimeManager").GetComponent<TimeManagerScript>();
        } catch(NullReferenceException e)
        {
            foundManagers = false;
            Debug.Log("Could not find one of the managers.");
        }
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        Debug.Log("Skip pressed.");
        if(foundManagers)
        {
            timeManager.kill();
        }
        SceneManager.LoadScene(0);
    }
}

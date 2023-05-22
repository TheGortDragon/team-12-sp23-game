using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButtonScript : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene(0);
    }
}

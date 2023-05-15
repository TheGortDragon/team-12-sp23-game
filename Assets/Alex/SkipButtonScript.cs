using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NextButtonScript : MonoBehaviour
{
    public ScoreboardLoadScript sls = null;
    // Start is called before the first frame update
    void Start()
    {
        sls = GameObject.Find("ScoreText").GetComponent<ScoreboardLoadScript>();
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        sls.nextPage();
    }
}

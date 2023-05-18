using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    // Start is called before the first frame update
    public void PlayGame(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

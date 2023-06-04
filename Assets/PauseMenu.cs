using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // set initially to invisible
    void Start() {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused) 
            {
                Resume();
            } else 
            {
                Pause();
            }
        }
    }

    public void Resume() 
    {
        //set pause menu invisible
        pauseMenuUI.SetActive(false);
        //allow for movement
        Time.timeScale = 1f;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            if (a.playOnAwake)
            {
                //a.Play();
                
            }
            a.UnPause();

        }
        //unpause 
        GameIsPaused = false;
    }

    void Pause() 
    {
        // set pause menu visible
        pauseMenuUI.SetActive(true);
        // don't allow for movement
        Time.timeScale = 0f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.Pause();
        }
        //pause game 
        GameIsPaused = true;
    }

    public void LoadMenu() 
    {
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("DavidTest", LoadSceneMode.Single);
    }
}

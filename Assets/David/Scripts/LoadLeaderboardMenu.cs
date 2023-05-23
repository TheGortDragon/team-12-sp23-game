using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLeaderboardMenu : MonoBehaviour
{
    public void loadLeaderboard() 
    {
        SceneManager.LoadScene("ScoreboardScreen", LoadSceneMode.Single);
    }
}

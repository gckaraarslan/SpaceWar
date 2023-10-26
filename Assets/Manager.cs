using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject gameIsOverPanel;
   
   
    public void showPanel(string value)        
    {
        gameIsOverPanel.SetActive(true);
        Invoke("stop", 1f);
        gameIsOverPanel.GetComponentInChildren<Text>().text = value;
    }

    void stop()
    {
        Time.timeScale = 0f;
    }
    public void playAgain()          
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

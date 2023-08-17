using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public GameObject scoreManager;
    void Start() 
    {
        pauseMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.transform.position = new Vector2(-10000,-10000);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }
    
    public void PauseGame() 
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.GetComponent<Canvas>().enabled = paused;
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.GetComponent<Canvas>().enabled = paused;
        pauseMenu.transform.position = new Vector2(-10000,-10000);

    }

    public void CreateMarkers() 
    {
        Debug.Log(scoreManager.GetComponent<ScoreManager>().scoreList);
        
    }
    
}

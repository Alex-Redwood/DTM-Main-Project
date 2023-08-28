using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public bool paused = false;
    public bool inMenu = true;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject scoreManager;
    public GameObject player;
    public int highScore = 0;
    public TextMeshProUGUI highScoreText;
    public float animationDelay;
    void Start()
    {
        pauseMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.transform.position = new Vector2(-10000, -10000);
        animationDelay = 1;
        startMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inMenu)
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (player.GetComponent<PlayerController>().currentHealth <= 0)
        {
            startMenu();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.GetComponent<Canvas>().enabled = paused;
        scoreManager.GetComponent<ScoreManager>().UpdateNoteStacking();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.GetComponent<Canvas>().enabled = paused;
        pauseMenu.transform.position = new Vector2(-10000, -10000);

    }

    public void startMenu()
    {
        Time.timeScale = 0;
        inMenu = true;
        mainMenu.GetComponent<Canvas>().enabled = true;
        if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelController>().roundNumber > highScore)
        {
            highScore = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelController>().roundNumber;
        }
        highScoreText.text = "Best Round: " + highScore;

    }


    public void endMenu()
    {
        Time.timeScale = 1;
        inMenu = false;
        mainMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.transform.position = new Vector2(-10000, -10000);
        resetLevel();
    }

    public void resetLevel()
    {
        player.GetComponent<PlayerController>().currentHealth = player.GetComponent<PlayerController>().maxHealth;
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelController>().resetAll();
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().DestroyAllNotes();

        while (GameObject.FindGameObjectsWithTag("InGameNote").Length > 0) {
            Destroy(GameObject.FindGameObjectsWithTag("InGameNote")[0]);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelController : MonoBehaviour
{
    public float newRoundTimer;
    public int roundNumber;
    public int enemiesNotSpawned;
    public GameObject enemyPrefab;
    public GameObject notePrefabDiamond;
    public GameObject scoreManager;
    public List<GameObject> enemies = new List<GameObject>();
    public TextMeshProUGUI newRoundHeader;
    public TextMeshProUGUI newRoundBody;
    

    // Start is called before the first frame update
    void Start()
    {
        roundNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0 && enemiesNotSpawned == 0) {
            roundNumber++;
            Debug.Log("New Round");
            newRoundHeader.enabled = true;
            newRoundBody.enabled = true;
            newRoundTimer = 4;
            string newRoundTitle = "Round " + roundNumber;

            newRoundHeader.text = newRoundTitle;
            newRound();
        }
        if ( 0 < newRoundTimer && newRoundTimer < 1) {
            newRoundHeader.enabled = false;
            newRoundBody.enabled = false;
            newRoundTimer = -1;
        } else {
            newRoundTimer -= Time.deltaTime;
        }
    }


    void newRound() {
        string newRoundInfo = "";
        // Sets number of enemies
        if (enemiesNotSpawned <= 4) {
            enemiesNotSpawned = roundNumber + 1;
            
        } else {
            enemiesNotSpawned = 2 * roundNumber - 3;
        }
        newRoundInfo = newRoundInfo + roundNumber + " Enemies<br>";


        // Creates a new note if needed
        if (roundNumber <= 5 || (roundNumber-7)%3 == 0) {
            Instantiate(notePrefabDiamond, transform.position,Quaternion.Euler(0, 0, 0));
            newRoundInfo = newRoundInfo + "+1 Note<br>";
        }

        // Increases BPM if needed
        if (roundNumber%5 == 0) {
            scoreManager.GetComponent<ScoreManager>().BPM = scoreManager.GetComponent<ScoreManager>().BPM + 10;
            newRoundInfo = newRoundInfo + "+10 BPM <br>";
        }
       

        newRoundBody.text = newRoundInfo;
    }
}
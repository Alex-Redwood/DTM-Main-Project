using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelController : MonoBehaviour
{
    public float newRoundTimer;
    public float spawnDelay;
    public int roundNumber;
    public int enemiesNotSpawned;
    public GameObject enemyPrefab;
    public GameObject[] notePrefabs;
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
        // Starts a new round if the old one is over
        if (enemies.Count == 0 && enemiesNotSpawned == 0 && Time.timeScale == 1) {
            roundNumber++;
            newRoundHeader.enabled = true;
            newRoundBody.enabled = true;
            newRoundHeader.color = new Color(newRoundHeader.color.r,newRoundHeader.color.g,newRoundHeader.color.b,1);
            newRoundBody.color = new Color(newRoundHeader.color.r,newRoundHeader.color.g,newRoundHeader.color.b,1);
            newRoundTimer = 4;
            string newRoundTitle = "Round " + roundNumber;

            newRoundHeader.text = newRoundTitle;
            newRound();
        }

        
        if ( 0 < newRoundTimer && newRoundTimer < 1) {
            newRoundHeader.color = new Color(newRoundHeader.color.r,newRoundHeader.color.g,newRoundHeader.color.b,Mathf.Abs(newRoundTimer));
            newRoundBody.color = new Color(newRoundHeader.color.r,newRoundHeader.color.g,newRoundHeader.color.b,Mathf.Abs(newRoundTimer));
            newRoundTimer -= Time.deltaTime;
        } else if ( 0 >= newRoundTimer && newRoundTimer != -1) {
            newRoundHeader.enabled = false;
            newRoundBody.enabled = false;
            newRoundTimer = -1;
        
        } else if (newRoundTimer != -1) {
            newRoundTimer -= Time.deltaTime;
        }
        
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null) {
                enemies.Remove(enemies[i]);
            }
        }
        
        // Spawns enemies with a random time interval between them, and that time gets shorter the more rounds there are
        if (newRoundTimer == -1 && enemiesNotSpawned != 0) {
            if (spawnDelay < 0 ) {
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                enemies.Add(newEnemy);
                enemiesNotSpawned -= 1;
                spawnDelay = Random.Range(1/roundNumber, 5/roundNumber);
            } else {
                spawnDelay -= Time.deltaTime;
            }
        }
    }


    void newRound() {
        string newRoundInfo = "";
        // Sets number of enemies
        if (roundNumber <= 4) {
            enemiesNotSpawned = roundNumber + 1;
            
        } else {
            enemiesNotSpawned = 2 * roundNumber - 3;
        }
        newRoundInfo = newRoundInfo + enemiesNotSpawned + " Enemies<br>";


        // Creates a new note if needed
        if (roundNumber <= 5 || (roundNumber-7)%3 == 0) {
            Instantiate(notePrefabs[Random.Range(0, notePrefabs.Length)], transform.position,Quaternion.Euler(0, 0, 0));
            newRoundInfo = newRoundInfo + "+1 Note<br>";
        }

        // Increases BPM if needed
        if (roundNumber%5 == 0) {
            scoreManager.GetComponent<ScoreManager>().BPM = scoreManager.GetComponent<ScoreManager>().BPM + 10;
            newRoundInfo = newRoundInfo + "+10 BPM <br>";
        }
       

        newRoundBody.text = newRoundInfo;
    }

    public void resetAll() {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null) {
                enemies.Remove(enemies[i]);
            }
        }

        while (enemies.Count > 0) {
            Destroy(enemies[0]);
            enemies.Remove(enemies[0]);
        }
        enemiesNotSpawned = 0;
        roundNumber = 0;        
    }
}

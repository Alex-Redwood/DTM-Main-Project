using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public ArrayList scoreList = new ArrayList(); 

    public float beatTimer = 0.0f;
    public float scoreTimer = 0.0f;
    public int scorePos = 0;
    public int scoreLen = 8;
    public int BPM = 90;
    public GameObject player;
    
    // scoreboard display variables
    public Image[] scoreMarkers;
    public Image scoreTracker;
    public GameObject scoreBar;

    // Plays sound
    public AudioSource playerAudio;
    public AudioClip beat;

    // Start is called before the first frame update
    void Start()
    {
        scoreList.Add("true");
        scoreList.Add("false");
        scoreList.Add("false");
        scoreList.Add("true");
        scoreList.Add("true");
        scoreList.Add("true");
        scoreList.Add("false");
        scoreList.Add("false");

        playerAudio = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        // Activates every second
        beatTimer += Time.deltaTime;
        if (beatTimer >= 1) {
             
            // Shoots projectile
            if (scoreList[scorePos] is "true") {
                player.GetComponent<PlayerController>().ShootProjectile();
            }
            
            playerAudio.PlayOneShot(beat, 1.0f);

            // Resets timer & increases count score position by 1
            beatTimer = 0;  
            scorePos += 1;
            
            if (scorePos >= scoreLen) {
                scorePos = 0;
            }
            
        }

        // Moves the score tracker line along the score
    }
}

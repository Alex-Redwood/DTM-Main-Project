using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public ArrayList scoreList = new ArrayList(); 

    // General score controlers
    public float scoreTimer = 0.0f;
    public bool offBeat = true;
    public int scorePos = 0;
    public int scoreLen = 4;
    public int BPM = 90;
    public GameObject player;
    
    // score display variables
    public Image[] scoreMarkers;
    public GameObject scoreTracker;
    public GameObject scoreBar;
    public float scoreBarWidth;


    // Plays sound
    public AudioSource playerAudio;
    public AudioClip beat;






    // Start is called before the first frame update
    void Start()
    {
        scoreList.Add("false");
        scoreList.Add("false");
        scoreList.Add("true");
        scoreList.Add("false");


        playerAudio = GetComponent<AudioSource>();

        // Gets width of the score bar
        scoreBarWidth = scoreBar.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Adds time to score timer
        scoreTimer += Time.deltaTime;
        scoreTracker.GetComponent<RectTransform>().anchoredPosition = new Vector2 ((scoreTimer * scoreBarWidth)/scoreLen-scoreBarWidth/2, scoreTracker.GetComponent<RectTransform>().anchoredPosition.y);
                                                                              
        // Resets the score timer if over the total time length of the bar seconds
        if (scoreTimer > scoreLen) {
            scoreTimer = 0;
        }

        Debug.Log(scoreTimer);

        
        if ((0 <= scoreTimer - Mathf.Floor(scoreTimer)) && (scoreTimer - Mathf.Floor(scoreTimer)) <= 0.5 && !offBeat) { // An elaborate if else expresion for 0 < x < 1
            offBeat = true;
        } else if ((0.5 < scoreTimer - Mathf.Floor(scoreTimer)) && (scoreTimer - Mathf.Floor(scoreTimer)) < 1 && offBeat){
            offBeat = false;
            player.GetComponent<PlayerController>().ShootProjectile();
        }
    }
}

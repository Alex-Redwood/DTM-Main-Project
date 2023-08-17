using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public string[] scoreList = new string[4];
    // General score controlers
    public float scoreTimer = 0.0f;
    public bool offBeat = true;
    public int beatNum = 0; // the INDEX of the note, so starts at 0
    public int scoreLen = 4;
    public int BPM = 60;
    public GameObject player;

    // score display variables
    public Image[] scoreMarkers;
    public GameObject[] scoreSlots;
    public GameObject scoreTracker;
    public GameObject scoreBar;
    public float scoreBarWidth;


    // Plays sound
    public AudioSource playerAudio;
    public AudioClip beat;




    // Start is called before the first frame update
    void Start()
    {
        scoreList[0] = "empty";
        scoreList[1] = "bullet1";
        scoreList[2] = "empty";
        scoreList[3] = "bullet1";

        playerAudio = GetComponent<AudioSource>();

        // Gets width of the score bar
        scoreBarWidth = scoreBar.GetComponent<RectTransform>().sizeDelta.x;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        // Adds time to score timer
        scoreTimer += Time.deltaTime * BPM / 60;
        scoreTracker.GetComponent<RectTransform>().anchoredPosition = new Vector2((scoreTimer * scoreBarWidth) / scoreLen - scoreBarWidth / 2, scoreTracker.GetComponent<RectTransform>().anchoredPosition.y);

        // Resets the score timers if over the total time length of the bar seconds
        if (scoreTimer >= scoreLen)
        {
            scoreTimer = 0;
            beatNum = 0;
        }



        if ((0 <= scoreTimer - Mathf.Floor(scoreTimer)) && (scoreTimer - Mathf.Floor(scoreTimer)) <= 0.5 && !offBeat)
        { // An elaborate if else expresion for 0 < x < 0.5
            offBeat = true;
        }
        else if ((0.5 < scoreTimer - Mathf.Floor(scoreTimer)) && (scoreTimer - Mathf.Floor(scoreTimer)) < 1 && offBeat)
        {
            offBeat = false;
            OnNote();
            beatNum += 1;
        }
    }

    public void UpdateScore()
    {
        for (int i = 0; i < scoreLen; i++)
        {
            if (scoreSlots[i].GetComponent<UINoteSlots>().noteList.Count == 0)
            {
                scoreMarkers[i].enabled = false;
            }
            else
            {
                scoreMarkers[i].enabled = true;
            }
        }
    }

    void OnNote()
    {
        if (scoreSlots[beatNum].GetComponent<UINoteSlots>().noteList.Count != 0)
        {
            player.GetComponent<PlayerController>().ShootProjectileDiamond();
            playerAudio.PlayOneShot(beat);
        }
    }

    public Vector2 MoveNote(Vector2 mousePos, Vector2 startingPos, GameObject note)
    {
        for (int i = 0; i < scoreLen; i++)
        {
            // If the mouse overlaps with the Note Slot
            if (scoreSlots[i].GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
            {
                // Removes note from any list it might have been in
                for (int j = 0; j < scoreLen; j++)
                {
                    scoreSlots[j].GetComponent<UINoteSlots>().RemoveNote(note);
                }

                // Adds note to new list
                scoreSlots[i].GetComponent<UINoteSlots>().AddNote(note);

                // Adds note to new list
                return scoreSlots[i].transform.position;
            }
        }

        // If not overlapping with any note slot, resets back to original position
        return startingPos;
    }

    public void UpdateNoteStacking()
    {
        for (int i = 0; i < scoreLen; i++)
        {
            scoreSlots[i].GetComponent<UINoteSlots>().ArrangeNotes();
        }
    }
}

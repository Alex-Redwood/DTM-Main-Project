using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{

    // General score controlers
    public float scoreTimer = 0.0f;
    public bool offBeat = true;
    public int beatNum = 0; // the INDEX of the note, so starts at 0
    public int scoreLen = 4;
    public int BPM = 60;
    public GameObject player;

    // score display variables
    public Image[] scoreMarkers;
    public GameObject scoreTracker;
    public GameObject scoreBar;
    public float scoreBarWidth;


    // Plays sound
    public AudioSource playerAudio;
    public AudioClip beat;


    // Slots management
    public List<GameObject> newNoteList = new List<GameObject>();
    public GameObject[] scoreSlots;
    public GameObject newNotePrefab;
    public GameObject newScoreSlot;
    public TextMeshProUGUI pickUpNotification;

    // Start is called before the first frame update
    void Start()
    {
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

        // Updates if the pick up with e notification should be shown
        PickUpNotification();

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

    // Shoots projectile once the score gets to it.
    void OnNote()
    {
        if (scoreSlots[beatNum].GetComponent<UINoteSlots>().noteList.Count != 0)
        {
            foreach (GameObject note in scoreSlots[beatNum].GetComponent<UINoteSlots>().noteList)
            {
                string noteType = note.GetComponent<UINote>().noteType;
                if (noteType == "aroundProjectile")
                {
                    player.GetComponent<PlayerController>().ShootProjectileAround();

                }
                else if (noteType is "diamondProjectile")
                {

                    player.GetComponent<PlayerController>().ShootProjectileDiamond();
                }
            }

            playerAudio.PlayOneShot(beat);
        }
    }

    // Moves notes from one place to another inside the UI.
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
                newNoteList.Remove(note);
                // Adds note to new list
                scoreSlots[i].GetComponent<UINoteSlots>().AddNote(note);
            
                // Adds note to new list
                return scoreSlots[i].transform.position;
            }
        }

        // If not overlapping with any note slot, resets back to original position
        return startingPos;
    }

    // Updates the positioning of active notes inside their score slots
    public void UpdateNoteStacking()
    {
        // Repositions notes in slots
        for (int i = 0; i < scoreLen; i++)
        {
            scoreSlots[i].GetComponent<UINoteSlots>().ArrangeNotes();
        }

        // Repositions notes in new notes slot
        for (int i = 0; i < newNoteList.Count; i++)
        {
            newNoteList[i].transform.position = new Vector2(newScoreSlot.transform.position.x, newScoreSlot.transform.position.y + i + 0.6f);
        }
    }

    // Triggered when you pick up a not in game
    public void PickUpNote(GameObject note)
    {
        GameObject newNote = Instantiate(newNotePrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectWithTag("PauseCanvas").transform);
        newNote.GetComponent<UINote>().noteType = note.GetComponent<InGameNote>().noteType;
        newNote.GetComponent<Image>().color = note.GetComponent<SpriteRenderer>().color;
        newNoteList.Add(newNote);
    }

    // Controls when the "pick up with e" notification is shown
    public void PickUpNotification()
    {
        pickUpNotification.enabled = false;
        foreach (GameObject note in GameObject.FindGameObjectsWithTag("InGameNote"))
        {
            if (Vector3.Distance(note.transform.position, player.transform.position) < 1.5)
            {
                pickUpNotification.enabled = true;
            }
        }
    }

    public void DestroyAllNotes() {
        for (int i = 0; i < scoreLen; i++)
        {
            scoreSlots[i].GetComponent<UINoteSlots>().EraseNotes();
        }
        UpdateScore();

        while (newNoteList.Count > 0) {
            Destroy(newNoteList[0]);
            newNoteList.Remove(newNoteList[0]);
        }
    }
}

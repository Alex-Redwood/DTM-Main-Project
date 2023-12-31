using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoteSlots : MonoBehaviour
{
    // A list of all notes in this slot
    public List<GameObject> noteList = new List<GameObject>();

    public void AddNote(GameObject note)
    {
        noteList.Add(note);
    }

    public void RemoveNote(GameObject note)
    {
        noteList.Remove(note);
    }

    public void ArrangeNotes() {
        for (int i=0; i < noteList.Count; i++) {
            noteList[i].transform.position = new Vector2(transform.position.x, transform.position.y - 1.3f + i);
        }
        
    }

    public void EraseNotes() {
        while (noteList.Count > 0) {
        Destroy(noteList[0]);
        noteList.Remove(noteList[0]);
        }
    }
}

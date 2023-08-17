using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoteSlots : MonoBehaviour
{
    // A list of all notes in this slot
    public List<GameObject> noteList = new List<GameObject>();
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNote(GameObject note)
    {
        
        noteList.Add(note);
    }

    public void RemoveNote(GameObject note)
    {
        noteList.Remove(note);
        Debug.Log("Removed Note");
    }
}

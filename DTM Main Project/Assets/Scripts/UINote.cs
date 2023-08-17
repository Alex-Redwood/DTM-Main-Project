using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UINote : MonoBehaviour
{
    // Defines dragging related
    public bool dragged;
    public Vector2 offset;
    public Vector2 notePos;
    public Vector2 mousePos;
    public BoxCollider2D hitbox;
    public Vector2 startingPos;
    public GameObject scoreManager;

    //Defines note related
    public string noteType;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Does all the dragging
        mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        notePos = new Vector2(transform.position.x, transform.position.y);
        if (hitbox.OverlapPoint(mousePos) && Input.GetMouseButtonDown(0))
        {
            dragged = true;
            offset = mousePos - notePos;
            startingPos = notePos;
        }

        if (Input.GetMouseButtonUp(0) && dragged)
        {
            dragged = false;
            transform.position = scoreManager.GetComponent<ScoreManager>().MoveNote(mousePos, startingPos, gameObject);
            scoreManager.GetComponent<ScoreManager>().UpdateScore();

        }

        if (dragged)
        {
            transform.position = mousePos - offset;
        }

    }
}

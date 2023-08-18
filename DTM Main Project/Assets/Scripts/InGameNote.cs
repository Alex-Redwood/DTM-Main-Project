using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameNote : MonoBehaviour
{
    public string noteType;

    void Update()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(transform.position, Player.transform.position) < 1.5)
        {
            if (Input.GetKeyDown("e"))
            {
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().PickUpNote(gameObject);
                Destroy(gameObject);
            }
        }
    }
}

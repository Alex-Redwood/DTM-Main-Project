using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameNote : MonoBehaviour
{
    public TextMeshProUGUI pickUpNotification;
    void Update()
    {
        foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < 1.5) {
                pickUpNotification.enabled = true;
                if (Input.GetKeyDown("e")) {
                    GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().PickUpNote(gameObject);
                    pickUpNotification.enabled = false;
                    Destroy(gameObject);
                }
            } else {
                pickUpNotification.enabled = false;
            }

            
        } 
    }
}

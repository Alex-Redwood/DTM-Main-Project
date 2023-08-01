using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C#
public class Pointer : MonoBehaviour
{
    // Defines the player to follow
    public GameObject player;
    public Vector3 offset = new Vector2(0,1);
    public Vector2 mousePos;
    public Vector3 playerMouse;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() { 
        // Finds vector pointing from player to mouse
        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        playerMouse = new Vector3 (mousePos.x, mousePos.y, player.transform.position.z) - player.transform.position;
        playerMouse.Normalize();

        // Shoots projectile
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (player.transform.position.y < transform.position.y) {
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg));
        } else {
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -1 * Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg));

        }
        
        }
       

    }

    // Update is called once per frame, but late - to stop jittering
        void LateUpdate()
    {
        transform.position = player.transform.position + playerMouse;
    }
}
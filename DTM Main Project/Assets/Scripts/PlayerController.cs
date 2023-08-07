using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Defines General Variables
    public float speed = 3.0f;
    public int dashTimer;
    public Vector2 dashDirection;
    public Vector2 movementInput;
    public Rigidbody2D rb;
    public Vector2 mousePos;
    public Vector2 playerMouse;

    // Defines Health Related Variables
    public int maxHealth;
    public int currentHealth;
    public Image[] hearts;

    // Defines Projectiles
    public GameObject projectileDiamondPrefab;
    public GameObject projectileAroundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        {
            maxHealth = hearts.Length;
            currentHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Controls basic WASD movement
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(movementInput.x, movementInput.y) * speed;

        // Updates health
        updateHealth();

        // Shoots projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectileAround();
        }
    }


    // Use FixedUpdate for physics related
    void FixedUpdate()
    {

    }

    // Manages collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Just keeping this here for later use
    }

    ////////////////////////////
    // MY OWN FUNCTIONS START //
    ////////////////////////////
    // Creates a basic projectile
    public void ShootProjectileDiamond()
    {
        // Finds vector pointing from player to mouse
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMouse = new Vector3(mousePos.x, mousePos.y, transform.position.z) - transform.position;
        playerMouse.Normalize();

        // Generates projectile
        if (transform.position.y < mousePos.y)
        {
            Instantiate(projectileDiamondPrefab, new Vector2(transform.position.x, transform.position.y) + playerMouse, Quaternion.Euler(0, 0, Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg));
        }
        else
        {
            Instantiate(projectileDiamondPrefab, new Vector2(transform.position.x, transform.position.y) + playerMouse, Quaternion.Euler(0, 0, -1 * Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg));
        }
    }

    public void ShootProjectileAround()
    {
        // Finds vector pointing from player to mouse
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMouse = new Vector3(mousePos.x, mousePos.y, transform.position.z) - transform.position;
        playerMouse.Normalize();

        // Generates projectile
        if (transform.position.y < mousePos.y)
        {
            Instantiate(projectileAroundPrefab, new Vector2(transform.position.x, transform.position.y) + playerMouse, Quaternion.Euler(0, 0, Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg));
        }
        else
        {
            Instantiate(projectileAroundPrefab, new Vector2(transform.position.x, transform.position.y) + playerMouse, Quaternion.Euler(0, 0, -1 * Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg));
        }
    }

    // Updates the health UI to reflect current health
    public Image[] updateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < currentHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        return hearts;
    }

    // Damages player when called
    public void damageSelf(int damageAmount) {
        currentHealth -= damageAmount;
    }
}

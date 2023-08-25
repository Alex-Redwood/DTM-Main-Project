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

    // Dash related variables
    public Vector2 currentDash;
    public Vector2 startingDash;

    // Pointer control
    public GameObject pointer;
    public Vector2 pointerPos;

    // Defines Health Related Variables
    public int maxHealth;
    public int currentHealth;
    public Image[] hearts;

    // Defines Projectiles
    public GameObject projectileDiamondPrefab;
    public GameObject projectileAroundPrefab;
    public GameObject projectileShotgunPrefab;

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
        // Controls dash

        //Controls pointer position
        PointerPosition();
    

        // Updates health
        updateHealth();
    }


    // Use FixedUpdate for physics related
    void FixedUpdate()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(movementInput.x, movementInput.y) * speed + currentDash;
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
        for (int i = 0; i < 8; i++)
        {
            Instantiate(projectileAroundPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, i * 45));
        }

    }


    public void ShootProjectileShotgun()
    {
        // Finds vector pointing from player to mouse
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMouse = new Vector3(mousePos.x, mousePos.y, transform.position.z) - transform.position;
        playerMouse.Normalize();

        // Shoots shotguns
        if (transform.position.y < mousePos.y)
        {
            Instantiate(projectileShotgunPrefab, new Vector2(transform.position.x, transform.position.y) + playerMouse, Quaternion.Euler(0, 0, Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg - 90f));
        }
        else
        {
            Instantiate(projectileShotgunPrefab, new Vector2(transform.position.x, transform.position.y) + playerMouse, Quaternion.Euler(0, 0, -1 * Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg - 90f));
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
    public void damageSelf(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

    Vector2 UpdateDash()
    {
        if (currentDash.x > 0)
        {
            currentDash = currentDash - Time.deltaTime * startingDash;
        }
        else
        {
            currentDash = new Vector2(0, 0);
        }

        return currentDash;

    }

    void PointerPosition() 
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMouse = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        playerMouse.Normalize();

        pointer.transform.position = (new Vector2(transform.position.x, transform.position.y) + playerMouse * 0.6f);

        if (transform.position.y < mousePos.y)
        {
            pointer.transform.rotation = Quaternion.Euler(0, 0, Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg - 90);
        }
        else
        {
            pointer.transform.rotation = Quaternion.Euler(0, 0, -1 * Mathf.Acos(playerMouse.x) * Mathf.Rad2Deg - 90);
        }
    }

}

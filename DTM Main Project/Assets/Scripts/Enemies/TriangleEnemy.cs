using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private GameObject target;
    Vector2 moveDirection; 
    public int damageAmount;
    public bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        // Sets direction of enemy
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        moveDirection = direction;

        // Sets spawn location
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        transform.position = new Vector2(Random.Range(-8f,8f), Random.Range(-4.2f,4.2f));
        while (Vector3.Distance(transform.position, Player.transform.position) < 3)
        {
            transform.position = new Vector2(Random.Range(-8f,8f), Random.Range(-4.2f,4.2f));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sets direction of enemy
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        moveDirection = direction;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;  
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().damageSelf(damageAmount);
            alive = false;
            Destroy(gameObject);
        }

        
    }

}

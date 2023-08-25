using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject target;
    Vector2 moveDirection; 
    public int damageAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(gameObject);
        }

        
    }

}

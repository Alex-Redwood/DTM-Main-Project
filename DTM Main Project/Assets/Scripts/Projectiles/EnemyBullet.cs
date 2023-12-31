using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damageAmount = 1;


    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().damageSelf(damageAmount);
        }

        Destroy(gameObject);
    }
}

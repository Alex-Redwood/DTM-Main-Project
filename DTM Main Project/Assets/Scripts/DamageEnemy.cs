using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public float damageAmount;

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<GenericEnemy>().damageSelf(damageAmount);
        }

        Destroy(gameObject);
    }
}

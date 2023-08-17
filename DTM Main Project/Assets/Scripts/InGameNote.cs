using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameNote : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().damageSelf(damageAmount);
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundProjectile : MonoBehaviour
{

    public float speed;
    public float deltaSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.right * 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed >= 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            speed -= deltaSpeed * Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
}

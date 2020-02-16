using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet_behavior : MonoBehaviour
{
    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.parent = null;
        Destroy(gameObject, 10);
    }

    public void Shoot_bullet(float speed,Transform pistola)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.velocity = pistola.TransformDirection(new Vector3(0, 0, speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

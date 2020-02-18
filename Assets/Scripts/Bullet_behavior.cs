using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet_behavior : NetworkBehaviour
{

    private Rigidbody rb;

    private Shoot_bullet father;


    // Start is called before the first frame update
    private void Start()
    {

        father = transform.root.GetComponent<Shoot_bullet>();
        rb = GetComponent<Rigidbody>();
        transform.parent = null;
        Destroy(gameObject, 10);
    }

    public void Shoot_bullet(float speed, Transform pistola)
    {
       
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.velocity = pistola.TransformDirection(new Vector3(0, 0, speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Vortex_Ball")
        {
            father.Cmd_createhole(collision.transform.position, collision.transform.rotation, gameObject, collision.transform.tag);
        }
        father.Cmd_Hitground(gameObject);
    }

}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet_behavior : NetworkBehaviour
{

    private Rigidbody rb;
    private Shoot_bullet father;

    [SerializeField]
    [Range(1000f, 200000f)]
    private float speed = 5000;

    private Vector3 dir;

    public GameObject GetBOwner()
    {
        return father.gameObject;

    }

    // Start is called before the first frame update
    private void Start()
    {

        father = transform.root.GetComponent<Shoot_bullet>();
        rb = GetComponent<Rigidbody>();
        transform.parent = null;
        Destroy(gameObject, 5);
        transform.localRotation = Camera.main.transform.localRotation;
        dir = Camera.main.transform.forward;
    }




    private void Update()
    {

        rb.AddForce(dir * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "buraco")
        {
            father.Cmd_createhole(collision.transform.position, collision.transform.rotation, gameObject, collision.transform.tag);
        }

    }

}

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
    [Range(10f, 200f)]
    private float speed = 20;

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
        Destroy(gameObject, 10);
        transform.rotation=father.transform.rotation;
        Debug.Log(new Vector3(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, 0));
    }




    private void Update()
    {
        rb.AddForce(transform.forward*2000);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Vortex_Ball")
        {
            father.Cmd_createhole(collision.transform.position, collision.transform.rotation, gameObject, collision.transform.tag);
        }

    }

}

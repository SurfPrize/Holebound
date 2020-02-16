using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet_behavior : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private GameObject buracoPrefab;

    // Start is called before the first frame update
    private void Start()
    {
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
            GameObject nburaco = Instantiate(buracoPrefab, transform);
            nburaco.transform.parent = null;
            switch (collision.transform.tag)
            {
                case "chao":
                    nburaco.transform.position = new Vector3(nburaco.transform.position.x, collision.transform.position.y, nburaco.transform.position.z);
                    break;
                case "parede":
                    nburaco.transform.position = new Vector3(nburaco.transform.position.x, nburaco.transform.position.y, collision.transform.position.z);
                    break;
                case "paredex":
                    nburaco.transform.position = new Vector3(collision.transform.position.x, nburaco.transform.position.y, nburaco.transform.position.z);
                    break;
            }
            nburaco.transform.localScale = Vector3.one;
            nburaco.transform.rotation = collision.transform.rotation;
            Destroy(nburaco, 10);
        }
        Destroy(gameObject);
    }

}

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
            nburaco.transform.eulerAngles = Vector3.zero;
            nburaco.transform.localScale = Vector3.one;
            Destroy(nburaco, 10);
        }
        Destroy(gameObject);
    }
}

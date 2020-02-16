using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_bullet : NetworkBehaviour
{

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float speed;

    [SerializeField]
    [Range(1, 10)]
    private int clip_size;

    private int cclip;

    [SerializeField]
    [Range(0.4f, 4f)]
    private float shootspeed;

    private float scooldown;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float reload_speed;

    private bool allowed;

    // Start is called before the first frame update
    private void Start()
    {
        cclip = clip_size;
    }


    private void OnMouseUp()
    {
        if (allowed)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            scooldown = shootspeed;
            allowed = false;
            StopCoroutine(reload());
            StartCoroutine(shoot_sp(scooldown));
        }

    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine(reload());
        }
    }

    private IEnumerator shoot_sp(float colldown)
    {
        float timer = 0.1f;
        scooldown -= timer;
        if (scooldown > 0)
        {
            yield return new WaitForSeconds(timer);
            StartCoroutine(shoot_sp(scooldown));
        }
        else
        {
            allowed = true;
        }

    }

    private IEnumerator reload()
    {
        if (cclip == clip_size)
            yield return null;

        yield return new WaitForSeconds(reload_speed);
        cclip++;
        if (cclip < clip_size)
            StartCoroutine(reload());
    }
}

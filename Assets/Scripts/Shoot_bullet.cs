using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_bullet : NetworkBehaviour
{

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject Spawnpoint;

    [SerializeField]
    [Range(1f, 15f)]
    private float bulletSpeed=5;

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

    private bool allowed = true;

    // Start is called before the first frame update
    private void Start()
    {
        cclip = clip_size;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine(reload());
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (allowed && cclip > 0)
            {
                Debug.Log("Shooting");
                GameObject newbullet=Instantiate(bullet, Spawnpoint.transform.position, Quaternion.identity,transform);
                newbullet.GetComponent<Bullet_behavior>().Shoot_bullet(bulletSpeed,Camera.main.transform);
                scooldown = shootspeed;
                cclip--;
                allowed = false;
                StopAllCoroutines();
                StartCoroutine(shoot_sp(scooldown));
            }
            else
            {
                Debug.Log("NO! " + cclip);
            }
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
            if (cclip != 0)
                allowed = true;
        }

    }

    private IEnumerator reload()
    {
        Debug.Log("Reloading " + cclip);
        if (cclip == clip_size)
        {
            Debug.Log("Clip full!");
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(reload_speed);
            cclip++;
            allowed = true;
            if (cclip < clip_size)
                StartCoroutine(reload());
        }
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Hud_methods))]
public class Shoot_bullet : NetworkBehaviour
{

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject Spawnpoint;

    private Hud_methods hdScript;


    [SerializeField]
    [Range(1f, 15f)]
    private float holesize = 5;

    [SerializeField]
    [Range(1, 10)]
    private int clip_size = 5;

    [SerializeField]
    private int cclip;

    [SerializeField]
    [Range(0.1f, 15f)]
    private float shootspeed = 5;

    [SerializeField]
    private GameObject buracoPrefab;

    private float scooldown;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float reload_speed = 0.4f;

    private bool isreloading = false;
    private bool allowed = true;

    // Start is called before the first frame update
    private void Start()
    {
        cclip = clip_size;
        hdScript = gameObject.GetComponent<Hud_methods>();
        foreach (Camera este in FindObjectsOfType<Camera>())
        {
            if (este.GetComponent<NetworkIdentity>() == null)
                este.gameObject.SetActive(false);
        }
        foreach (AudioListener este in FindObjectsOfType<AudioListener>())
        {
            if (este.GetComponent<NetworkIdentity>() == null)
                Destroy(este);
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {


            if (Input.GetKeyUp(KeyCode.R) && !isreloading)
            {
                isreloading = true;
                StartCoroutine(reload());
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (allowed && cclip > 0)
                {
                    Cmd_Shoot();
                }
                else
                {
                    Debug.Log("NO! " + cclip);
                }
            }
        }
    }

    [Command]
    public void Cmd_createhole(Vector3 parede, Quaternion rot, GameObject bala, string tagg)
    {
        GameObject nburaco = Instantiate(buracoPrefab, bala.transform);
        NetworkServer.Spawn(nburaco);
        nburaco.transform.parent = null;
        switch (tagg)
        {
            case "chao":
                nburaco.transform.position = new Vector3(nburaco.transform.position.x, parede.y, nburaco.transform.position.z);
                break;
            case "parede":
                nburaco.transform.position = new Vector3(nburaco.transform.position.x, nburaco.transform.position.y, parede.z);
                break;
            case "paredex":
                nburaco.transform.position = new Vector3(parede.x, nburaco.transform.position.y, nburaco.transform.position.z);
                break;
            default:
                break;
        }
        nburaco.transform.localScale = Vector3.one * holesize;
        nburaco.transform.rotation = rot;
        NetworkServer.Destroy(bala);
        Destroy(bala);
    }

    [Command]
    public void Cmd_Destroy(GameObject este)
    {
        NetworkServer.Destroy(este);
        Destroy(este);
    }

    [Command]
    private void Cmd_Shoot()
    {
        Debug.Log("Shooting");
        GameObject newbullet = Instantiate(bullet, Spawnpoint.transform.position, transform.rotation, transform) as GameObject;
        NetworkServer.Spawn(newbullet);

        scooldown = shootspeed;
        StartCoroutine(shoot_sp(scooldown));
        cclip--;
        hdScript.Updatehud(cclip);


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

        if (cclip == clip_size)
        {
            isreloading = false;
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(reload_speed);
            cclip++;
            hdScript.Updatehud(cclip);
            allowed = true;
            if (cclip < clip_size)
                StartCoroutine(reload());
            else
                isreloading = false;
        }

    }
}

using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Hud_methods))]
public class Shoot_bullet : NetworkBehaviour
{
    private Controlosps4 Inputc;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject Spawnpoint;

    private Hud_methods hdScript;

    private List<GameObject> hidden_objs = new List<GameObject>();
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

    //[SerializeField]
    //private GameObject buracoPrefab;

    private float scooldown;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float reload_speed = 0.4f;

    private bool isreloading = false;
    private bool allowed = true;


    private void OnEnable()
    {
        Inputc = new Controlosps4();
        Inputc.PlayercontrolsPS4.Shoot.performed += HandleShot;
        Inputc.PlayercontrolsPS4.Reload.performed += HandleReload;
        Inputc.PlayercontrolsPS4.Shoot.Enable();
        Inputc.PlayercontrolsPS4.Reload.Enable();
    }

    private void OnDisable()
    {
        Inputc.PlayercontrolsPS4.Shoot.performed -= HandleShot;
        Inputc.PlayercontrolsPS4.Reload.performed -= HandleReload;
        Inputc.PlayercontrolsPS4.Shoot.Disable();
        Inputc.PlayercontrolsPS4.Reload.Disable();
    }
    private void HandleReload(InputAction.CallbackContext obj)
    {
        if (isLocalPlayer)
        {
            if (!isreloading)
            {
                isreloading = true;
                StartCoroutine(Reload());
            }
        }
    }

    private void HandleShot(InputAction.CallbackContext obj)
    {
        if (isLocalPlayer)
        {
            Debug.Log("Shoot");
            if (allowed&& !isreloading && cclip > 0)
            {
                Cmd_Shoot();
            }
            else
            {
                Debug.Log("NO! " + cclip);
            }
        }
    }

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
        //if (isLocalPlayer)
        //{

        //    if (reload_pressed && !isreloading)
        //    {
        //        isreloading = true;
        //        StartCoroutine(Reload());
        //    }

        //    if (shoot_pressed != 0)
        //    {
        //        Debug.Log("Shoot");
        //        if (allowed && cclip > 0)
        //        {
        //            Cmd_Shoot();
        //        }
        //        else
        //        {
        //            Debug.Log("NO! " + cclip);
        //        }
        //    }
        //}
    }

    //[Command]
    //public void Cmd_createhole(Vector3 parede, Quaternion rot, GameObject bala, string tagg)
    //{
    //    GameObject nburaco = Instantiate(buracoPrefab, bala.transform);
    //    NetworkServer.Spawn(nburaco);
    //    nburaco.transform.parent = null;
    //    switch (tagg)
    //    {
    //        case "chao":
    //            nburaco.transform.position = new Vector3(nburaco.transform.position.x, parede.y, nburaco.transform.position.z);
    //            break;
    //        case "parede":
    //            nburaco.transform.position = new Vector3(nburaco.transform.position.x, nburaco.transform.position.y, parede.z);
    //            break;
    //        case "paredex":
    //            nburaco.transform.position = new Vector3(parede.x, nburaco.transform.position.y, nburaco.transform.position.z);
    //            break;
    //        default:
    //            break;
    //    }
    //    nburaco.transform.localScale = Vector3.one * holesize;
    //    nburaco.transform.rotation = rot;
    //    NetworkServer.Destroy(bala);
    //    Destroy(bala);
    //}
    public void Start_hide(GameObject obj)
    {
        StartCoroutine(Hide_hole(obj, obj.GetComponent<Renderer>().material));
    }

    public IEnumerator Hide_hole(GameObject obj, Material obj_mat)
    {
        float mult = 0.95f;
        if (obj_mat.color.a > 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            obj_mat.color = new Color(obj_mat.color.r * mult, obj_mat.color.g * mult, obj_mat.color.b * mult, obj_mat.color.a * mult);
            obj.transform.localScale *= 0.95f;
            StartCoroutine(Hide_hole(obj, obj_mat));
        }
        else
        {
            hidden_objs.Add(obj);
            obj.SetActive(false);
            yield return null;
        }

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
        StartCoroutine(Shoot_sp(scooldown));
        cclip--;
        hdScript.Updatehud(cclip);
    }

    private IEnumerator Shoot_sp(float cooldown)
    {
        float timer = 0.1f;
        scooldown -= timer;
        if (scooldown > 0)
        {
            yield return new WaitForSeconds(timer);
            StartCoroutine(Shoot_sp(scooldown));
        }
        else
        {
            if (cclip != 0)
                allowed = true;
        }

    }

    private IEnumerator Reload()
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
                StartCoroutine(Reload());
            else
                isreloading = false;
        }

    }
}

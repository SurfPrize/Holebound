using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet_behavior : NetworkBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private GameObject buracoPrefab;

    private NetworkIdentity player;

    private NetworkIdentity ni;


    // Start is called before the first frame update
    private void Start()
    {

        player = transform.root.GetComponent<NetworkIdentity>();
        ni = GetComponent<NetworkIdentity>();
        rb = GetComponent<Rigidbody>();
        transform.parent = null;
        Destroy(gameObject, 10);
    }

    private void Assingauth()
    {
        ni.AssignClientAuthority(player.GetComponent<NetworkIdentity>().connectionToClient);
    }

    private void Resignauth()
    {
        ni.RemoveClientAuthority(player.GetComponent<NetworkIdentity>().connectionToClient);
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
            Assingauth();
            Cmd_createhole(collision.gameObject);
            Resignauth();
        }
        Destroy(gameObject);
    }

    [Command]
    private void Cmd_createhole(GameObject parede)
    {
        GameObject nburaco = Instantiate(buracoPrefab, transform);
        NetworkServer.Spawn(nburaco,player.connectionToServer);
        NetworkServer.Spawn
        nburaco.transform.parent = null;
        switch (parede.transform.tag)
        {
            case "chao":
                nburaco.transform.position = new Vector3(nburaco.transform.position.x, parede.transform.position.y, nburaco.transform.position.z);
                break;
            case "parede":
                nburaco.transform.position = new Vector3(nburaco.transform.position.x, nburaco.transform.position.y, parede.transform.position.z);
                break;
            case "paredex":
                nburaco.transform.position = new Vector3(parede.transform.position.x, nburaco.transform.position.y, nburaco.transform.position.z);
                break;
        }
        nburaco.transform.localScale = Vector3.one;
        nburaco.transform.rotation = parede.transform.rotation;
        Destroy(nburaco, 10);
    }
}

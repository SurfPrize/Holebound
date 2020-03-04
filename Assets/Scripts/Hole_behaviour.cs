using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_behaviour : MonoBehaviour
{
    public GameObject Owner;

    [SerializeField]
    [Range(1, 10)]
    private readonly int lifetime = 5;

    [SerializeField]
    [Range(0.4f, 5f)]
    private readonly float rotspeed = 3;

    private void Awake()
    {
        Assign_Owner(transform.parent.root.gameObject);
        StartCoroutine(rotate());
        StartCoroutine(lifespan());
    }

    // Start is called before the first frame update
    void Assign_Owner(GameObject ow)
    {
        Owner = ow.GetComponent<Bullet_behavior>().GetBOwner();
    }

    private IEnumerator lifespan()
    {

        yield return new WaitForSeconds(lifetime);
        Owner.GetComponent<Shoot_bullet>().Cmd_Destroy(gameObject);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Scorer>() != null)
        {
            other.GetComponent<Scorer>().Last_player_touch = Owner;
        }
    }

    private IEnumerator rotate()
    {
        yield return new WaitForSeconds(0.0001f);
        transform.Rotate(rotspeed, rotspeed, rotspeed);
        StartCoroutine(rotate());
    } 
}

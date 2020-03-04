using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekndestroy : NetworkBehaviour
{
    private List<Transform> children = new List<Transform>();
    bool run;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform este in transform)
        {
            children.Add(este);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        run = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("OLA");
        if (other.tag == "buraco")
        {
            run = true;
            StartCoroutine(Remove_obj(other.transform.position,other.GetComponent<Hole_behaviour>().Owner.GetComponent<Shoot_bullet>()));
        }
    }

    IEnumerator Remove_obj(Vector3 pos, Shoot_bullet pl)
    {
        foreach (Transform este in children)
        {
            if (Vector3.Distance(este.position, pos) < 3)
            {
                children.Remove(este);
                pl.Cmd_Destroy(este.gameObject);
                break;
            }
        }
        yield return new WaitForSeconds(0.5f);
        if (run)
        {
            Remove_obj(pos,pl);
        }

    }

}

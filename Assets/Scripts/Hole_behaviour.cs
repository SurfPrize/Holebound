using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole_behaviour : MonoBehaviour
{
    public GameObject Owner;

    [SerializeField]
    [Range(1, 10)]
    private int lifetime = 5;

    private void Awake()
    {
        Assign_Owner(transform.parent.root.gameObject);
       
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
}

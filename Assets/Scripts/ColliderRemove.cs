using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemove : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0)
        {
            other.gameObject.layer = 8;
            Debug.Log("HES IN");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 0)
        {
            other.gameObject.layer = 9;
        }
    }

}

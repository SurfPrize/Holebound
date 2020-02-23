using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemove : NetworkBehaviour
{
    private Collider current;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0)
        {
            current = other;
            StartCoroutine(falling(other));
            Debug.Log("HES IN");

        }
    }


    private void OnDestroy()
    {
        current.gameObject.layer = 9;
    }

    private IEnumerator falling(Collider este)
    {
        este.gameObject.layer = 8;
        yield return new WaitForSeconds(0.3f);
        este.gameObject.layer = 9;
    }
}

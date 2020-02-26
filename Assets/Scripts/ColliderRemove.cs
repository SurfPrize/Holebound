using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemove : NetworkBehaviour
{
    private Collider current;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0 && other.gameObject.layer != 12)
        {
            current = other;
            StartCoroutine(Falling(other));
        }
    }


    public void OnDestroy()
    {
        if (current != null)
            current.gameObject.layer = 9;
    }

    private IEnumerator Falling(Collider este)
    {
        este.gameObject.layer = 8;
        yield return new WaitForSeconds(0.3f);
        este.gameObject.layer = 9;
    }
}

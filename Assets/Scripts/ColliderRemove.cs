using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 11)
        {
            other.gameObject.layer = 8;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 11)
        {
            other.gameObject.layer = 9;
        }
    }

}

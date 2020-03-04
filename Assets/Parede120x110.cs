using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parede120x110 : MonoBehaviour
{
    public GameObject cubo_prefab;

    private void Start()
    {
        for (int i = 0; i < 60; i++)
        {
            for (int e= 0; e < 55; e++)
            {
                Instantiate(cubo_prefab, new Vector3(i*2, e*2, 0f),Quaternion.identity, gameObject.transform);

            }
        }
    }
}

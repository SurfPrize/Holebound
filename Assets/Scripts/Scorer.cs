using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Scorer : MonoBehaviour
{

    private NetworkStartPosition[] spawns;
    private GameObject _Last_player_touch;
    public GameObject Last_player_touch
    {
        get => _Last_player_touch;
        set
        {
            if (value != gameObject)
                _Last_player_touch = value;
        }
    }

    [SerializeField]
    [Range(1, 5)]
    private int lives = 3;

    private Shoot_bullet commands;

    [SerializeField]
    [Range(-200, -5)]
    private float outofbounds_limit = -50;
    // Start is called before the first frame update
    void Start()
    {
        spawns = FindObjectsOfType<NetworkStartPosition>();
        commands = gameObject.GetComponent<Shoot_bullet>();
        StartCoroutine(Bounds_check());
    }

    private IEnumerator Bounds_check()
    {
        yield return new WaitForSeconds(0.5f);
        if (transform.position.y < outofbounds_limit)
        {
            transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;

            if (Last_player_touch == null)
            {
                //aqui faz algo tipo uma animacao que mataste
            }

            lives--;

            Last_player_touch = null;
            foreach (Hole_behaviour este in FindObjectsOfType<Hole_behaviour>())
            {
                commands.Cmd_Destroy(este.gameObject);
            }
            if (lives == 0)
            {
                //Game over
            }
        }
        StartCoroutine(Bounds_check());
    }

}

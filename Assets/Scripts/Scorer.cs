using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Scorer : MonoBehaviour
{

    private NetworkStartPosition[] spawns;
    private Hole_behaviour Last_hole;

    [SerializeField]
    [Range(1, 5)]
    private int lives = 3;

    private Shoot_bullet commands;

    [SerializeField]
    [Range(-80, -5)]
    private readonly float outofbounds_limit = -20;
    // Start is called before the first frame update
    void Start()
    {
        spawns = FindObjectsOfType<NetworkStartPosition>();
        commands = gameObject.GetComponent<Shoot_bullet>();
    }

    private IEnumerator Bounds_check()
    {
        yield return new WaitForSeconds(0.5f);
        if (transform.position.y < outofbounds_limit)
        {
            transform.position = spawns[Random.Range(0,spawns.Length)].transform.position;
            
            if (Last_hole.Owner != gameObject)
            {
                //aqui faz algo tipo uma animacao que mataste
            }

            lives--;

            Last_hole = null;
            foreach(Hole_behaviour este in FindObjectsOfType<Hole_behaviour>())
            {
                commands.Cmd_Destroy(este.gameObject);
            }
            if (lives == 0)
            {
                //Game over
            }
        }
    }

    public void change_last_touch(Hole_behaviour este)
    {
        Last_hole = este;
    }
}

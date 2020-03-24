using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu_cmds : MonoBehaviour
{
    public void Exit_game()
    {
        Application.Quit();
    }

    public void Create_Room(GameObject prefab_room)
    {
        GameObject novo=Instantiate(prefab_room);
        
    }

    public void Search_Room(GameObject prefab_search)
    {
        Instantiate(prefab_search);
    }
}

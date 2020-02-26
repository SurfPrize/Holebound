using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;
public class Hud_methods : NetworkBehaviour
{
    

    [SerializeField]
    private TextMeshProUGUI ammo_text;


    public void Updatehud(int ammo)
    {
        ammo_text.text = "";
        if (ammo == 0)
        {
            ammo_text.text = "No Ammo!";
        }
        else
        {
            ammo_text.text = "Ammo ";
            for (int i = 0; i < ammo; i++)
            {
                ammo_text.text +="|";
            }
        }
    }

}



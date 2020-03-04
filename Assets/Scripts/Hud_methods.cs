using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;
public class Hud_methods : NetworkBehaviour
{
    float deltaTime;

    [SerializeField]
    private TextMeshProUGUI ammo_text;

    [SerializeField]
    private TextMeshProUGUI fps_text;

    private void Start()
    {
        Application.targetFrameRate = 15000;
    }
    private void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0F;
        float fps = 1.0F / deltaTime;
        fps_text.text = "FPS " + fps.ToString("F0") + "\n";
        
    }

    
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



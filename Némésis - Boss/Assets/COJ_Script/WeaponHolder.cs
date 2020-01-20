using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public PlayerStat player;

    public Image defaultGun;
    public Image miniGun;
    public Image shotGun;
    public Image sniperGun;
    public Image edgeGun;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        defaultGun.enabled = false;
        miniGun.enabled = false;
        shotGun.enabled = false;
        sniperGun.enabled = false;
        edgeGun.enabled = false;
    }

    void Update()
    {
        changeWeapon();   
    }

    void changeWeapon()
    {
        if(player.weaponR == 0)
        {
            defaultGun.enabled = true;
            miniGun.enabled = false;
            shotGun.enabled = false;
            sniperGun.enabled = false;
            edgeGun.enabled = false;
        }
        else if (player.weaponR == 1)
        {
            defaultGun.enabled = false;
            miniGun.enabled = false;
            shotGun.enabled = false;
            sniperGun.enabled = true;
            edgeGun.enabled = false;
        }
        else if (player.weaponR == 2)
        {
            defaultGun.enabled = false;
            miniGun.enabled = true;
            shotGun.enabled = false;
            sniperGun.enabled = false;
            edgeGun.enabled = false;
        }
        else if (player.weaponR == 3)
        {
            defaultGun.enabled = false;
            miniGun.enabled = false;
            shotGun.enabled = true;
            sniperGun.enabled = false;
            edgeGun.enabled = false;
        }
        else if (player.weaponR == 4)
        {
            defaultGun.enabled = false;
            miniGun.enabled = false;
            shotGun.enabled = false;
            sniperGun.enabled = false;
            edgeGun.enabled = true;
        }


    }
}

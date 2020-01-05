﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerBehaviour : MonoBehaviour
{
    public ItemSpawn spawnItemlist;
    // Start is called before the first frame update
    void Start()
    {
        spawnItemlist = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemSpawn>();
        if (gameObject.CompareTag("ItemSpawner") || gameObject.CompareTag("ShopItemSpawner"))
        {
            SpawnItem();
        }
        else if (gameObject.CompareTag("WeaponSpawner") || gameObject.CompareTag("ShopWeaponSpawner"))
        {
            SpawnWeapon();
        }
    }

    //Spawn un item
    void SpawnItem()
    {
        int i = Random.Range(0, spawnItemlist.itemList.Count);
        GameObject spawnedItem = Instantiate(spawnItemlist.itemList[i], gameObject.transform.position, Quaternion.identity);
        if (gameObject.CompareTag("ShopItemSpawner"))
        {
            spawnedItem.tag = "ShopItem";
        }
        else
        {
            spawnedItem.tag = "item";
        }
    }
    //Spawn une arme
    void SpawnWeapon()
    {
        int i = Random.Range(0, spawnItemlist.weaponList.Count);
        GameObject spawnedWeapon = Instantiate(spawnItemlist.weaponList[i], gameObject.transform.position, Quaternion.identity);
        if (gameObject.CompareTag("ShopWeaponSpawner"))
        {
            spawnedWeapon.tag = "ShopWeapon";
        }
        else
        {
            spawnedWeapon.tag = "item";
        }
    }
}

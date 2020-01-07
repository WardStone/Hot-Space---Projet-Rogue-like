using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public List<GameObject> weaponList = new List<GameObject>();
    GameObject itemHolder;
    GameObject weaponHolder;

    // Start is called before the first frame update
    void Awake()
    {
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
        FillTheList();
    }

    //Rempli la list avec les items attachés à l'ItemHolder
    void FillTheList()
    {
        for (int i = 0; i < itemHolder.transform.childCount; i++)
        {
            itemList.Add(itemHolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < weaponHolder.transform.childCount; i++)
        {
            weaponList.Add(weaponHolder.transform.GetChild(i).gameObject);
        }
    }
}

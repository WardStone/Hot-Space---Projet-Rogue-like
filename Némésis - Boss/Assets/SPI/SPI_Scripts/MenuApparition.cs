using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuApparition : MonoBehaviour
{
    public GameObject menu;
    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");
        menu.SetActive(false);
    }

    void Appart()
    {
        Debug.Log("Marche putain");
        menu.SetActive(true);
    }
}

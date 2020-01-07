using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuApparition : MonoBehaviour
{
    public GameObject menu;
    public GameObject logo;
    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");
        logo = GameObject.Find("Logo");
        logo.SetActive(false);
        menu.SetActive(false);
    }

    void Appart()
    {
        logo.SetActive(true);
        menu.SetActive(true);
    }
}

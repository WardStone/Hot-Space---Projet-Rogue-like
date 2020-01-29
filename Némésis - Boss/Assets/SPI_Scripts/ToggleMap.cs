using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMap : MonoBehaviour
{
    public bool mapStatus;
    
    void Update()
    {
        if (Input.GetButtonDown("Map"))
        {
            if (mapStatus)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                mapStatus = false;
            } else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                mapStatus = true;
            }
        }
    }
}

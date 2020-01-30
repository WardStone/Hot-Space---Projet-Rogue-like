using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchButton : MonoBehaviour
{
    public bool canInteract = false;

    // Update is called once per frame

    private void Start()
    {
        canInteract = false;
    }
    void Update()
    {
        if(canInteract == true)
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }
}

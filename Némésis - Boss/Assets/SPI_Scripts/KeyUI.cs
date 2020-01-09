using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    public GameObject keyVoidRight;
    public GameObject keyVoidLeft;
    public GameObject keyGetRight;
    public GameObject keyGetLeft;
    void Start()
    {
        keyVoidRight.SetActive(true);
        keyVoidLeft.SetActive(true);
        keyGetRight.SetActive(false);
        keyGetLeft.SetActive(false);
    }

    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("LeftKey").Length == 0 )
        {
            keyVoidLeft.SetActive(false);
            keyGetLeft.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("RightKey").Length == 0)
        {
            keyVoidRight.SetActive(false);
            keyGetRight.SetActive(true);

        }
    }
}

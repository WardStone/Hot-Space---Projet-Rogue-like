using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadingScreen : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportManager : MonoBehaviour
{
    public GameObject tp;
    public GameObject rootRoom;
    public GameObject tpManager;


    void Start()
    {
        rootRoom = GameObject.FindGameObjectWithTag("RootRoom");
        tpManager = rootRoom.transform.GetChild(0).gameObject;// enfant de la salle racine qui sert juste de detecteur/ lock
    }
    void Update()
    {
        if (tpManager.CompareTag("lock"))
        {
            tp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            tp.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            tp.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            tp.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            tp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            tp.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            tp.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            tp.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}

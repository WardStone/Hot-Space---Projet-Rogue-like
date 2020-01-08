using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdeath : MonoBehaviour
{
    public bool wannaRestartBoi;
    public GameObject player;
    void Start()
    {

        
    }
    void Update()
    {
        if (wannaRestartBoi)
        {
            if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Pause"))
            {

            }
        }
    }

    public IEnumerator playerDies()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //player.transform.GetChild(tamère).gameObject.SetActive(true);
        Time.timeScale = 0f;
        wannaRestartBoi = true;
        yield break;
    }
}

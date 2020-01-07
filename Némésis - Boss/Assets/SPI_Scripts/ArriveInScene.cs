using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveInScene : MonoBehaviour
{
    public GameObject player;
    public GameObject loadingScreenAnim;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        loadingScreenAnim = GameObject.FindGameObjectWithTag("loadScreen");
        StartCoroutine(loadingScreen());
    }

    IEnumerator loadingScreen()
    {
        player.SetActive(false);
        //loadingScreenAnim.SetActive(true);//Set loading screen active true
        yield return new WaitForSeconds(4f);
        player.SetActive(true);
        player.transform.position = gameObject.transform.localPosition;
        loadingScreenAnim.SetActive(false);//Set loading screen active true
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveInScene : MonoBehaviour
{
    public GameObject player;
    public GameObject loadingScreenAnim;

    private void Start()
    {
        LaunchScene();
    }
    public void LaunchScene()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        loadingScreenAnim = GameObject.FindGameObjectWithTag("loadScreen");
        StartCoroutine(loadingScreen());
    }

    IEnumerator loadingScreen()
    {
        player.SetActive(false);
        Debug.Log("loadscreen");
        //loadingScreenAnim.SetActive(true);//Set loading screen active true
        yield return new WaitForSeconds(4.5f);
        player.SetActive(true);
        player.transform.position = gameObject.transform.localPosition;
        loadingScreenAnim.SetActive(false);//Set loading screen active true
    }
}

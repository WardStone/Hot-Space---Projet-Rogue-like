using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LihgtFishScript : MonoBehaviour
{

    public GameObject GlobalLight;

    private void Start()
    {
        GlobalLight.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectiles"))
        {
           
            StartCoroutine(LightOn());
        }
    }

    IEnumerator LightOn()
    {
        GlobalLight.SetActive(true);
        yield return new WaitForSeconds(5f);
        GlobalLight.SetActive(false);
        Destroy(gameObject);
    }
}

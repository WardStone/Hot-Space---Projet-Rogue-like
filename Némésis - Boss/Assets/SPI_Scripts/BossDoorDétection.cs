using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorDétection : MonoBehaviour
{
    public bool canOpenBoss;

    void Start()
    {
        canOpenBoss = false;
    }

    void Update()
    {
        if (canOpenBoss & Input.GetButtonDown("Interact"))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canOpenBoss = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            canOpenBoss = false;
    }
}

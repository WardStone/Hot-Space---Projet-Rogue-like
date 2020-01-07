using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public bool canPick=false;
   
    void Update()
    {
        if (canPick && Input.GetButtonDown("Interact"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canPick = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPick = false;
    }
}

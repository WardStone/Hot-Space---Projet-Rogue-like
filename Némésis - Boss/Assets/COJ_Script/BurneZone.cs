using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurneZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fadeout());
    }

    IEnumerator Fadeout()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}

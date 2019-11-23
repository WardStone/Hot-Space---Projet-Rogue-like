using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrkRockBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}

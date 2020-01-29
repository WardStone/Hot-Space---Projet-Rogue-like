using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimerDestroy());
    }

    IEnumerator TimerDestroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}

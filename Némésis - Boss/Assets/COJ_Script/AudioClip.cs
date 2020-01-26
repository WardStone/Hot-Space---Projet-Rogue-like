using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClip : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroySound());
    }
    
    IEnumerator destroySound()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}

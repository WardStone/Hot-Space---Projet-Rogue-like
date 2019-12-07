using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpcDmgBoxBehavior : MonoBehaviour
{

    public float boxLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("lifeSpan");
    }

    IEnumerator lifeSpan()
    {
        yield return new WaitForSeconds(boxLifeSpan);
        Destroy(gameObject);
    }
}

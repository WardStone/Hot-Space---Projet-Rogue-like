using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject rocks = Instantiate(myPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

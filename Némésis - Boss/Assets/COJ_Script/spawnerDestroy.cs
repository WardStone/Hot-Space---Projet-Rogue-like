using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}

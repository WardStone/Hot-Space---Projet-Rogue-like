using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOBJECTNEEDED : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyOBJ());
    }

    // Update is called once per frame
  IEnumerator destroyOBJ()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

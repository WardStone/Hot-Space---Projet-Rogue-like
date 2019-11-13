using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowToStyRock : MonoBehaviour
{
    public GameObject stayingRock;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        GameObject rock = Instantiate(stayingRock, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
        
    }
}

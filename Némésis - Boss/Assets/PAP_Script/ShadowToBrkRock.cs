using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowToBrkRock : MonoBehaviour
{
    public GameObject brkRock;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        GameObject rock = Instantiate(brkRock, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}

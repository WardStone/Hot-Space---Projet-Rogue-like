using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRockBehavior : MonoBehaviour
{
    public float fallSpeed;
    public float safeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SafeTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, fallSpeed, 0);
    }

    IEnumerator SafeTimer()
    {
        yield return new WaitForSeconds(safeTime);
        transform.gameObject.tag = "FallRock";
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Shadow"))
        {
            Destroy(gameObject);
        }
    }
}

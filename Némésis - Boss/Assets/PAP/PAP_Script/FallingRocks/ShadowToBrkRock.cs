using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowToBrkRock : MonoBehaviour
{
    public GameObject brkRock;
    public GameObject fallRock;
    public int fallHeight;
    public float safeTime;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fallRock, new Vector3(transform.position.x, transform.position.y + fallHeight, 0), Quaternion.identity);
        StartCoroutine(SafeTimer());
    }

    private void Update()
    {
        transform.localScale += new Vector3(0.014f, 0.005f, 0);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FallRock"))
        {
            Instantiate(brkRock, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }

    }

    IEnumerator SafeTimer()
    {
        yield return new WaitForSeconds(safeTime);
        transform.gameObject.tag = "Shadow";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRockBehavior : MonoBehaviour
{
    public float fallSpeed;
    public float safeTime;
    private float originalPosiy;

    // Start is called before the first frame update
    void Start()
    {
        originalPosiy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = transform.position + new Vector3(0, fallSpeed, 0);

    }


    public void OnTriggerEnter2D(Collider2D other)
    {

        if ((transform.position.y <= originalPosiy - 9) && (other.gameObject.CompareTag("Shadow")))
        {
            transform.gameObject.tag = "FallRock";

            Destroy(gameObject);
        }
    }
}

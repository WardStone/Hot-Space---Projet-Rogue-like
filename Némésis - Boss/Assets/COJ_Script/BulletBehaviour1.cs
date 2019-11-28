using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss") || other.CompareTag("bossLeftArm01") || other.CompareTag("bossRightArm01") || other.CompareTag("bossHead01") || other.CompareTag("StyRock") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

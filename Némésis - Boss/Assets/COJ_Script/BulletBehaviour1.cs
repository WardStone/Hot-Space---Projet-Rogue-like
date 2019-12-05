using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour1 : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss") || other.CompareTag("bossLeftArm01") || other.CompareTag("bossRightArm01") || other.CompareTag("bossHead01") || other.CompareTag("StyRock") || other.CompareTag("BossHeart"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour1 : MonoBehaviour
{
    public GameObject impact;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss") || other.CompareTag("bossLeftArm01") || other.CompareTag("bossLeftArm02") || other.CompareTag("bossLeftArm03") || other.CompareTag("bossRightArm01") || other.CompareTag("bossRightArm02") || other.CompareTag("bossRightArm03") || other.CompareTag("bossHead01") || other.CompareTag("bossHead02") || other.CompareTag("bossHead03") || other.CompareTag("StyRock") || other.CompareTag("BossHeart") || other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            Instantiate(impact, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyRockBehavior : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DmgOnFall());
    }


    IEnumerator DmgOnFall()
    {
        yield return new WaitForSeconds(0.1f);
        transform.gameObject.tag = "StyRock";
    }

    void HitByBullet()
    {
        health -= 5;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

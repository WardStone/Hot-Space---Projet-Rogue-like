using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXScript : MonoBehaviour
{
    public GameObject explosionGen;

    private void Start()
    {
        if (gameObject.CompareTag("BossExplosionGen"))
        {
            StartCoroutine(BossExplosionMaking());
        }
    }
    void destroyFX()
    {
        Destroy(gameObject);
    }

    IEnumerator BossExplosionMaking()
    {
        for (int i = 0; i < 150; i++)
        {
            Vector3 randomVec = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f)); 
            Instantiate(explosionGen, gameObject.transform.position - randomVec, Quaternion.identity);
            yield return new WaitForSeconds(0.025f);
        }
        Destroy(gameObject);
    }

}

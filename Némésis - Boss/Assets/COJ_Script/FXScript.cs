using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXScript : MonoBehaviour
{
    public PlayerControllerScript player;
    public GameObject explosionGen;
    public int maxExplosion;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        if (gameObject.CompareTag("BossExplosionGen"))
        {
            maxExplosion = 150;
            StartCoroutine(BossExplosionMaking());
        }
        if (gameObject.CompareTag("BossPartExplosion"))
        {
            maxExplosion = 25;
            StartCoroutine(BossExplosionMaking());
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("GunFlash"))
        {
            gameObject.transform.position = player.firePoint.transform.position;
        }
    }

    void destroyFX()
    {
        Destroy(gameObject);
    }

    IEnumerator BossExplosionMaking()
    {
        for (int i = 0; i < maxExplosion; i++)
        {
            Vector3 randomVec = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f)); 
            Instantiate(explosionGen, gameObject.transform.position - randomVec, Quaternion.identity);
            yield return new WaitForSeconds(0.025f);
        }
        Destroy(gameObject);
    }

}
